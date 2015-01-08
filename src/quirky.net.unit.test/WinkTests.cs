using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using quirky.net.unit.test.Extensions;

namespace quirky.net.unit.test
{
    [TestFixture]
    public class WinkTests
    {
        private Configuration config = new Configuration();

        [Test]
        public void Login()
        {
            var client = new WinkClient(config);
            client.Login().ContinueWith(r =>
            {
                var result = r.Result as quirky.net.Entities.Response.BaseResponse;
                var login = r.Result as quirky.net.Entities.Response.LoginResponse;

                TestAssert.Success(result);
                TestAssert.IsStatusCode(result, StatusCode.Created);
                Assert.IsNotNullOrEmpty(login.data.access_token);
                Assert.IsNotNullOrEmpty(login.data.refresh_token);
                Assert.IsNotNullOrEmpty(login.data.token_type);
            }).Wait();
        }


        [Test]
        public void ListDevices()
        {
            var client = new WinkClient(config);
            client.Login().ContinueWith(t =>
            {
                client.ListDevices().ContinueWith(r =>
                {
                    TestAssert.SuccessOK(r.Result);
                    var device = r.Result.Devices.FirstOrDefault();
                    Assert.IsNotNull(device);

                }).Wait();
            }).Wait();
        }

        [Test]
        public void UpdateDevice()
        {
            var newDeviceName = System.Guid.NewGuid().ToString();
            var client = new WinkClient(config);
            client.Login().ContinueWith(t =>
            {
                TestAssert.Success(t.Result);
                client.ListDevices().ContinueWith(r =>
                {
                    TestAssert.SuccessOK(r.Result);
                    var device = r.Result.Devices.FirstOrDefault();
                    Assert.IsNotNull(device);
                    device.Name = newDeviceName;
                    client.UpdateDevice(device).ContinueWith(up =>
                    {
                        TestAssert.Success(up.Result);
                    }).Wait();
                }).Wait();
            }).Wait();
        }
        [Test]
        public void RefreshDevice()
        {
            var newDeviceName = System.Guid.NewGuid().ToString();
            var client = new WinkClient(config);
            client.Login().ContinueWith(t =>
            {
                TestAssert.Success(t.Result);
                client.ListDevices().ContinueWith(r =>
                {
                    TestAssert.SuccessOK(r.Result);
                    var device = r.Result.Devices.FirstOrDefault();
                    Assert.IsNotNull(device);
                    device.Name = newDeviceName;
                    client.RefreshDevice(device).ContinueWith(up =>
                    {
                        TestAssert.Success(up.Result);
                    }).Wait();
                }).Wait();
            }).Wait();
        }
        [Test]
        public void GetUser()
        {
            var newDeviceName = System.Guid.NewGuid().ToString();
            var client = new WinkClient(config);
            client.Login().ContinueWith(t =>
            {
                TestAssert.Success(t.Result);
                client.GetUser().ContinueWith(u =>
                {
                    TestAssert.Success(u.Result);
                }).Wait();
            }).Wait();
        }

    }
}