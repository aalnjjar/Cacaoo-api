﻿using ChocolateDelivery.BLL;
using ChocolateDelivery.DAL;
using ChocolateDelivery.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Npgsql;

namespace ChocolateDelivery.UI.Components;

public class UCDropDown : ViewComponent
{
    private AppDbContext context;
    private readonly IConfiguration _config;
    private string logPath = "";
    public UCDropDown(AppDbContext cc, IConfiguration config)
    {
        context = cc;
        _config = config;
        logPath = _config.GetValue<string>("ErrorFilePath"); // "Information"
    }

    public IViewComponentResult Invoke(UCProperties properties)
    {
        var dt = new DataTable();
        try
        {
            ViewBag.Id = properties.Id;
            if (!string.IsNullOrEmpty(properties.Name))
                ViewBag.Name = properties.Name;
            else
                ViewBag.Name = properties.Id;
            ViewBag.Value = properties.Value;
            ViewBag.CssClass = properties.CSSClass;
            ViewBag.Placeholder = properties.Placeholder;
            ViewBag.Is_Required = properties.Is_Required;
            ViewBag.Multiple = properties.Multiple;
            ViewBag.ErrorMessage = "This field is Required";
            var lang = HttpContext.Session.GetString("Culture") ?? Language.English;
            if (properties.Is_Required && properties.Error_Label_Id != null && properties.Error_Label_Id != 0)
            {
                var commonBC = new CommonService(context, logPath);

                if (lang == Language.Arabic)
                {
                    //adding ar-labels class defined in site.css for arabic labels to customise fonts
                    properties.CSSClass = properties.CSSClass + " ar-texts";
                }
                var appLabels = SessionHelper.GetObjectFromJson<List<SM_LABELS>>(HttpContext.Session, "AppLabels");
                if (appLabels != null)
                {
                    var labelDM = appLabels.Where(x => x.Label_Id == properties.Error_Label_Id).FirstOrDefault();
                    if (labelDM != null)
                    {
                        ViewBag.ErrorMessage = lang == Language.Arabic ? labelDM.A_Label_Name : labelDM.L_Label_Name;

                    }
                    else
                    {
                        labelDM = commonBC.GetLabel((int)properties.Error_Label_Id);
                        if (labelDM != null)
                        {
                            ViewBag.ErrorMessage = lang == Language.Arabic ? labelDM.A_Label_Name : labelDM.L_Label_Name;

                        }
                    }
                }
                else
                {
                    var labels = commonBC.GetAllLabels();
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "AppLabels", labels);
                    var labelDM = labels.Where(x => x.Label_Id == properties.Error_Label_Id).FirstOrDefault();
                    if (labelDM != null)
                    {
                        ViewBag.ErrorMessage = lang == Language.Arabic ? labelDM.A_Label_Name : labelDM.L_Label_Name;

                    }
                }

                if (string.IsNullOrEmpty(ViewBag.ErrorMessage))
                {
                    ViewBag.ErrorMessage = "This field is Required";
                }
            }


            if (properties.List_Id != null)
            {
                var listBC = new ListService(context, logPath);
                var findDTO = listBC.GetList((long)properties.List_Id);
                var findDetails = listBC.GetListFields((long)properties.List_Id);
                if (lang == Language.Arabic)
                {
                    findDetails = findDetails.Where(x => (x.Field_Language == Field_Language_Types.Undefined || x.Field_Language == Field_Language_Types.Arabic)).ToList();
                }
                else
                {
                    findDetails = findDetails.Where(x => (x.Field_Language == Field_Language_Types.Undefined || x.Field_Language == Field_Language_Types.English)).ToList();
                }

                if (findDTO != null)
                {
                    if (!string.IsNullOrEmpty(findDTO.Find_Return_Columns))
                    {
                        var returnColumns = findDTO.Find_Return_Columns.Split(",").Select(long.Parse).ToList();
                        findDetails = findDetails.Where(x => returnColumns.Contains(x.Field_Id)).ToList();
                    }
                    var restaurant_id = HttpContext.Session.GetInt32("VendorId");

                    ViewBag.List_Name = findDTO.List_Name_E;
                    ViewBag.Update_URL = findDTO.Update_URL;
                    ViewData["ListDetails"] = findDetails;
                    var connectionString = _config.GetValue<string>("ConnectionStrings:DefaultConnection");

                    var selectstmt = "select ";
                    var isGroupBy = findDTO.Contain_Group_By_Clause ?? false;
                    var groupbyclause = new List<string>();
                    var i = 0;
                    foreach (var findDetail in findDetails)
                    {
                        if (i == findDetails.Count - 1)
                        {
                            if (!isGroupBy || (findDetail.Field_Group_By_Type == null || findDetail.Field_Group_By_Type == List_Fields_Group_Types.Normal_Field))
                            {
                                selectstmt += (string.IsNullOrEmpty(findDetail.Table_Id) ? "" : findDetail.Table_Id + ".") + findDetail.Table_Field_Name + " as " + "'" + findDetail.Field_Name_E + "'";

                                if (!string.IsNullOrEmpty(findDetail.Group_By_Field_Name))
                                    groupbyclause.Add((string.IsNullOrEmpty(findDetail.Table_Id) ? "" : findDetail.Table_Id + ".") + findDetail.Group_By_Field_Name);
                            }
                            else if (findDetail.Field_Group_By_Type == List_Fields_Group_Types.Sum_Field)
                            {
                                selectstmt += "Sum(" + (string.IsNullOrEmpty(findDetail.Table_Id) ? "" : findDetail.Table_Id + ".") + findDetail.Table_Field_Name + ")";
                            }
                            else if (findDetail.Field_Group_By_Type == List_Fields_Group_Types.Max_Field)
                            {
                                selectstmt += "Max(" + (string.IsNullOrEmpty(findDetail.Table_Id) ? "" : findDetail.Table_Id + ".") + findDetail.Table_Field_Name + ")";
                            }

                        }
                        else
                        {

                            if (!isGroupBy || (findDetail.Field_Group_By_Type == null || findDetail.Field_Group_By_Type == List_Fields_Group_Types.Normal_Field))
                            {
                                selectstmt += (string.IsNullOrEmpty(findDetail.Table_Id) ? "" : findDetail.Table_Id + ".") + findDetail.Table_Field_Name + " as " + "'" + findDetail.Field_Name_E + "'" + ",";
                                if (!string.IsNullOrEmpty(findDetail.Group_By_Field_Name))
                                    groupbyclause.Add(findDetail.Table_Id + "." + findDetail.Group_By_Field_Name);
                            }
                            else if (findDetail.Field_Group_By_Type == List_Fields_Group_Types.Sum_Field)
                            {
                                selectstmt += "Sum(" + (string.IsNullOrEmpty(findDetail.Table_Id) ? "" : findDetail.Table_Id + ".") + findDetail.Table_Field_Name + ")" + ",";
                            }
                            else if (findDetail.Field_Group_By_Type == List_Fields_Group_Types.Max_Field)
                            {
                                selectstmt += "Max(" + (string.IsNullOrEmpty(findDetail.Table_Id) ? "" : findDetail.Table_Id + ".") + findDetail.Table_Field_Name + ")" + ",";
                            }

                        }

                        i++;
                    }
                    selectstmt += " from " + findDTO.From_Clause;
                    selectstmt += " where 1=1 ";
                    if (!string.IsNullOrEmpty(findDTO.Where_Clause)) {
                        selectstmt += " and " + findDTO.Where_Clause;
                    }
                    if (findDTO.Contain_Entity_Id)
                    {
                        if (string.IsNullOrEmpty(findDTO.Entity_Id_Alias))
                            selectstmt += " and restaurant_id = " + restaurant_id;
                        else
                            selectstmt += " and " + findDTO.Entity_Id_Alias + ".restaurant_id = " + restaurant_id;
                    }
                    if (!string.IsNullOrWhiteSpace(properties.Additional_Where_Clause))
                    {
                        selectstmt += " and " + properties.Additional_Where_Clause;
                    }

                    //if (!string.IsNullOrEmpty(findDTO.Where_Clause))
                    //{
                    //    selectstmt += " where " + findDTO.Where_Clause;

                    //    if (findDTO.Contain_Entity_Id)
                    //    {
                    //        selectstmt += " and entity_id = " + entity_id;
                    //    }
                    //    if (!string.IsNullOrWhiteSpace(properties.Additional_Where_Clause)) {
                    //        selectstmt += " and " + properties.Additional_Where_Clause;
                    //    }
                    //}
                    //else
                    //{
                    //    if (findDTO.Contain_Entity_Id)
                    //    {
                    //        selectstmt += " where entity_id = " + entity_id;
                    //    }
                    //}

                    if (isGroupBy)
                    {
                        selectstmt += " group by " + string.Join(",", groupbyclause);
                    }
                    if (!string.IsNullOrEmpty(findDTO.Order_By_Clause))
                    {
                        selectstmt += " order by " + findDTO.Order_By_Clause;
                    }

                    if (findDTO.Write_SQL_Log == true)
                    {
                        Helpers.WriteToFile(logPath, selectstmt, true);

                    }
                    if (findDTO.Is_StoredProcedure == true)
                    {

                        using (var con = new NpgsqlConnection(connectionString))
                        {
                            using (var cmd = new NpgsqlCommand(findDTO.StoredProcedure_Name, con))
                            {
                                if (findDTO.Command_Timeout != null)
                                {
                                    cmd.CommandTimeout = (int)findDTO.Command_Timeout;
                                }
                                using (var da = new NpgsqlDataAdapter(cmd))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    //var findParameters = findDTO.X_SM_REPORT_PARAMETERS.ToList();
                                    //for (int j = 0; j < findParameters.Count; j++)
                                    //{
                                    //    cmd.Parameters.Add(findParameters[j].Parameter_Name, GetSqlDbType(findParameters[j].Parameter_DB_Type)).Value = paramterValues[j];
                                    //}
                                    da.Fill(dt);
                                    //da.Fill(dt);
                                }



                            }
                        }


                    }
                    else
                    {
                        using (var con = new NpgsqlConnection(connectionString))
                        {
                            using (var cmd = new NpgsqlCommand(selectstmt.ConvertToPgsqlQuery()))
                            {
                                cmd.Connection = con;
                                using (var sda = new NpgsqlDataAdapter(cmd))
                                {
                                    sda.Fill(dt);
                                }
                            }
                        }

                    }

                }
            }
        }
        catch (Exception ex)
        {
            Helpers.WriteToFile(logPath, "error in uc dropdown for list id:" + properties.List_Id, true);
            Helpers.WriteToFile(logPath, ex.ToString(), true);

        }

        return View(dt);
    }
}