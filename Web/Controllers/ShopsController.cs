﻿using Microsoft.AspNetCore.Mvc;
using ShoppingList.Core;
using ShoppingList.Data.Helper;
using ShoppingList.Data.Products;
using ShoppingList.Data.Shops;
using ShoppingList.Web.Helper;
using ShoppingList.Web.Models.Shops;

namespace ShoppingList.Web.Controllers
{
    public class ShopsController : Controller
    {
        private readonly IShopRepository _shopRepository;
        private readonly IProductMaintainer _productMaintainer;
        public const string ControllerName = "Shops";

        public ShopsController(IShopRepository shopRepository, IProductMaintainer productMaintainer)
        {
            _shopRepository = shopRepository;
            _productMaintainer = productMaintainer;
        }

        /// <summary>
        /// Returns all products in a shop.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> AllProducts(string shopName)
        {
            IList<IProductEntity> products = await _shopRepository.AllProductsForShop(shopName);
            IList<ProductModel> productModels = products.Select(ModelMapper.ToProductModel).ToList();

            ExistingProductsModel model = new ExistingProductsModel { Products = productModels };
            return PartialView("_ExistingProducts", model);
        }

        /// <summary>
        /// Returns HTML select options for all products in a shop.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ProductOptions(string shopName)
        {
            IList<IProductEntity> products = await _shopRepository.AllProductsForShop(shopName);
            IList<ProductModel> productModels = products.Select(ModelMapper.ToProductModel).ToList();

            ExistingProductOptionsModel model = new ExistingProductOptionsModel { Products = productModels };
            return PartialView("_ExistingProductOptions", model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterProduct([FromBody]RegisterProductModel model)
        {
            await _productMaintainer.RegisterProductAsync(model.ShopName, model.NewProductName, model.NextProductName);
            return Ok();
        }
    }
}
