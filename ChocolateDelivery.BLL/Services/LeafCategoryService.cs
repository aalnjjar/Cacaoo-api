using ChocolateDelivery.DAL;

namespace ChocolateDelivery.BLL;

public class LeafCategoryService
{
    private readonly AppDbContext _context;

    public LeafCategoryService(AppDbContext benayaatEntities)
    {
        _context = benayaatEntities;

    }
      
    public SM_Leaf_Categories CreateLeafCategory(SM_Leaf_Categories categoryDM)
    {
        try
        {
            var query = (from o in _context.sm_leaf_categories
                where o.Leaf_Category_Id == categoryDM.Leaf_Category_Id
                select o).FirstOrDefault();

            if (query != null)
            {
                query.Sub_Category_Id = categoryDM.Sub_Category_Id;
                query.Leaf_Category_Name_E = categoryDM.Leaf_Category_Name_E;
                query.Leaf_Category_Name_A = categoryDM.Leaf_Category_Name_A;
                query.Leaf_Category_Desc_E = categoryDM.Leaf_Category_Desc_E;
                query.Leaf_Category_Desc_A = categoryDM.Leaf_Category_Desc_A;
                query.Show = categoryDM.Show;
                query.Sequence = categoryDM.Sequence;
                query.Updated_By = categoryDM.Updated_By;
                query.Updated_Datetime = categoryDM.Updated_Datetime;
                if (!string.IsNullOrEmpty(categoryDM.Image_URL))
                {
                    query.Image_URL = categoryDM.Image_URL;
                }
                query.Background_Color = categoryDM.Background_Color;
            }
            else
            {
                _context.sm_leaf_categories.Add(categoryDM);
            }
            _context.SaveChanges();
        }

        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        return categoryDM;
    }

    public SM_Leaf_Categories? GetLeafCategory(int category_id)
    {
        var area = new SM_Leaf_Categories();
        try
        {


            area = (from o in _context.sm_leaf_categories
                where o.Leaf_Category_Id == category_id
                select o).FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        return area;
    }

    public List<SM_Leaf_Categories> GetAllLeafCategories()
    {

        var leafCategoriesList = new List<SM_Leaf_Categories>();
        try
        {
            leafCategoriesList = (from o in _context.sm_leaf_categories                           
                select o).ToList();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
        return leafCategoriesList;
    }

}