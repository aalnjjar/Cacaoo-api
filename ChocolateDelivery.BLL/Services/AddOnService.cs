﻿using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public class AddOnService : IAddOnService
{
    private readonly AppDbContext _context;

    public AddOnService(AppDbContext context)
    {
        _context = context;
    }


    public SM_Restaurant_AddOns CreateAddOn(SM_Restaurant_AddOns addonDM)
    {
        try
        {
            var query = (from o in _context.sm_restaurant_addons
                where o.AddOn_Id == addonDM.AddOn_Id
                select o).FirstOrDefault();

            if (query != null)
            {
                query.AddOn_Type_Id = addonDM.AddOn_Type_Id;
                query.AddOn_Name_E = addonDM.AddOn_Name_E;
                query.AddOn_Name_A = addonDM.AddOn_Name_A;
                query.AddOn_Desc_E = addonDM.AddOn_Desc_E;
                query.AddOn_Desc_A = addonDM.AddOn_Desc_A;
                query.Show = addonDM.Show;
                query.Sequence = addonDM.Sequence;
                query.Updated_By = addonDM.Updated_By;
                query.Updated_Datetime = addonDM.Updated_Datetime;
                if (!string.IsNullOrEmpty(addonDM.Image_URL))
                {
                    query.Image_URL = addonDM.Image_URL;
                }
            }
            else
            {
                _context.sm_restaurant_addons.Add(addonDM);
            }
            _context.SaveChanges();
        }

        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        return addonDM;
    }

    public SM_Restaurant_AddOns? GetAddOn(long addon_id)
    {
        var area = new SM_Restaurant_AddOns();
        try
        {


            area = (from o in _context.sm_restaurant_addons
                where o.AddOn_Id == addon_id
                select o).FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        return area;
    }
}