﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnLineStore.Common;
using OnLineStore.Core.BusinessLayer.Responses;
using OnLineStore.Core.EntityLayer.Warehouse;
using OnLineStore.WebAPI.Controllers;
using OnLineStore.WebAPI.UnitTests.Mocks;
using Xunit;

namespace OnLineStore.WebAPI.UnitTests
{
    public class WarehouseControllerTests
    {
        [Fact]
        public async Task TestGetProductsTestAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetWarehouseService(userInfo, nameof(TestGetProductsTestAsync));
            var controller = new WarehouseController(LoggingHelper.GetLogger<WarehouseController>(), service);

            // Act
            var response = await controller.GetProductsAsync() as ObjectResult;
            var value = response.Value as IPagedResponse<Product>;

            service.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count() > 0);
        }

        [Fact]
        public async Task TestGetInventoryByProductTestAsync()
        {
            // Arrange
            var userInfo = IdentityMocker.GetCustomerIdentity().GetUserInfo();
            var service = ServiceMocker.GetWarehouseService(userInfo, nameof(TestGetInventoryByProductTestAsync));
            var controller = new WarehouseController(LoggingHelper.GetLogger<WarehouseController>(), service);
            var productID = 1;
            var warehouseID = "W0001";

            // Act
            var response = await controller.GetProductInventoryAsync(productID, warehouseID) as ObjectResult;
            var value = response.Value as IListResponse<ProductInventory>;

            service.Dispose();

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count() > 0);
        }
    }
}
