using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MVCDemoLabpart1.Models
{
    public class ProductBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string ProductId = bindingContext.HttpContext.Request.Form["ProductId"];
            string Name = bindingContext.HttpContext.Request.Form["Name"];
            string Price = bindingContext.HttpContext.Request.Form["Price"];
            string Description = bindingContext.HttpContext.Request.Form["Description"];
            string ImagePath = bindingContext.HttpContext.Request.Form["ImagePath"];
            string CategoryId = bindingContext.HttpContext.Request.Form["CategoryId"];

            //Check CategoryId
            //if (string.IsNullOrEmpty(CategoryId) || CategoryId == "0")
            //{
            //    bindingContext.ModelState.AddModelError("CategoryId", "Please select a valid category.");
            //    //bindingContext.Result = ModelBindingResult.Failed(); // Indicate failure
            //    //return Task.CompletedTask; // Return early 
            //}

            //Create New Price 
            decimal newPrice = Convert.ToDecimal(Price) + (Convert.ToDecimal(Price) * 0.1M);
            int Id;
            int.TryParse(ProductId, out Id);
            Products newProduct = new Products
            {
                ProductId = Id,
                Name = Name,
                Price = newPrice,
                Description = Description,
                ImagePath = ImagePath ?? string.Empty,
                CategoryId = int.Parse(CategoryId)
            }; 
            bindingContext.Result = ModelBindingResult.Success(newProduct);
            return Task.CompletedTask;
        }
    }
}

