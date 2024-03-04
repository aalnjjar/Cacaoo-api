﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ChocolateDelivery.DAL;

public class SM_Products
{
    [Key] public long Product_Id { get; set; }
    public int Main_Category_Id { get; set; }
    public int Product_Type_Id { get; set; }
    public int Sub_Category_Id { get; set; }
    public long Restaurant_Id { get; set; }
    public string Product_Name_E { get; set; } = string.Empty;
    public string? Product_Name_A { get; set; } = string.Empty;
    public string? Product_Desc_E { get; set; } = string.Empty;
    public string? Product_Desc_A { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Image_URL { get; set; } = string.Empty;
    public bool Show { get; set; }
    public bool Publish { get; set; }
    public int Sequence { get; set; } = 1;
    public decimal Stock_In_Hand { get; set; }
    public int? Created_By { get; set; }
    public DateTime? Created_Datetime { get; set; }
    public int? Updated_By { get; set; }
    public DateTime? Updated_Datetime { get; set; }
    public Guid Row_Id { get; set; }
    public string? Comments { get; set; } = string.Empty;
    public string? Admin_Comments { get; set; } = string.Empty;
    public int Status_Id { get; set; } = 1;
    public long? Brand_Id { get; set; }
    public string? Short_Desc_E { get; set; } = string.Empty;
    public string? Short_Desc_A { get; set; } = string.Empty;
    public string? Nutritional_Facts_E { get; set; } = string.Empty;
    public string? Nutritional_Facts_A { get; set; } = string.Empty;
    public decimal Rating { get; set; }
    public int Total_Ratings { get; set; }
    public bool Is_Exclusive { get; set; }
    public bool Is_Catering { get; set; }
    public bool Is_Catering_Menu_Product { get; set; }
    public int PreparationTime { get; set; }
    public bool IsCustomizable { get; set; }

    [NotMapped] public IFormFile? Image_File { get; set; }
    [NotMapped] public long Category_Id { get; set; }
    [NotMapped] public string? Brand_Name_E { get; set; }
    [NotMapped] public string? Brand_Name_A { get; set; }
    [NotMapped] public string? DeliveryTime { get; set; }
    [NotMapped] public bool Is_Favorite { get; set; }
    [NotMapped] public bool Is_Gift_Product { get; set; }
    [NotMapped] public string[] Occasion_Ids { get; set; } = Array.Empty<string>();

    [NotMapped] public List<SM_Product_AddOns> SM_Product_AddOns { get; set; } = new();

    [NotMapped] public List<SM_Product_Branches> SM_Product_Branches { get; set; } = new();

    [NotMapped] public List<SM_Product_Catering_Products> SM_Product_Catering_Products { get; set; } = new();
}