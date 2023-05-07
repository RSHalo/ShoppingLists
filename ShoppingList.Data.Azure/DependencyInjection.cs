﻿using Microsoft.Extensions.DependencyInjection;
using ShoppingList.Core.Products;
using ShoppingList.Data.Azure.Products;
using ShoppingList.Data.Azure.Shops;
using ShoppingList.Data.Shops;

namespace ShoppingList.Data.Azure
{
    public static class DependencyInjection
    {
        public static void AddAzureDataAccess(this IServiceCollection services)
        {
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IShopRepository, ShopRepository>();
            services.AddSingleton<IProductMaintainer, ProductMaintainer>();
        }
    }
}
