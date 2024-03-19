using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChocolateDelivery.DAL;

public class SM_Product_Types
{
    [Key]
    public long Type_Id { get; set; }
    public string Type_Name_E { get; set; } = string.Empty;
    public string? Type_Name_A { get; set; } = string.Empty;
    public string? Type_Desc_E { get; set; } = string.Empty;
    public string? Type_Desc_A { get; set; } = string.Empty;
    public string? Image_URL { get; set; } = string.Empty;
    [NotMapped]
    public string Image_Full_URL => !string.IsNullOrEmpty(Image_URL) 
        ? "https://chocopedia.s3.me-central-1.amazonaws.com/" + Image_URL
        : string.Empty;
    public bool Show { get; set; }
    public int Sequence { get; set; } = 1;
    public int? Created_By { get; set; }
    public DateTime? Created_Datetime { get; set; }
    public int? Updated_By { get; set; }
    public DateTime? Updated_Datetime { get; set; }
}