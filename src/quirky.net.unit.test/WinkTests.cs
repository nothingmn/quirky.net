using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace quirky.net.unit.test
{
    [TestFixture]
    public class WinkTests
    {
        private Configuration config = new Configuration();

        [Test]
        public void LoginTest()
        {
            var client = new WinkClient(config);
            client.Login().ContinueWith(r =>
            {
                var result = r.Result as quirky.net.Entities.Response.BaseResponse;
                Assert.IsTrue(result.Success);
            }).Wait();
        }


        [Test]
        public void ListDevicesTest()
        {
            var client = new WinkClient(config);
            client.Login().ContinueWith(t =>
            {
                client.ListDevices().ContinueWith(r =>
                {
                    var result = r.Result as quirky.net.Entities.Response.BaseResponse;
                    Assert.IsTrue(result.Success);
                }).Wait();
            }).Wait();
        }

    }
}