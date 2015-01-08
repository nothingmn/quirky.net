using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using quirky.net.Entities.Response;

namespace quirky.net.unit.test.Extensions
{
    public static class TestAssert
    {

        public static void HeaderHasValue(BaseResponse result, string header, string message = null)
        {
            if (header == null) throw new ArgumentNullException("header");
            if (message == null) message = string.Format("Failed:{0}", result.Message);
            var existingHeader = (from h in result.Headers where h.Key == header select h.Value);
            Assert.IsNotNull(existingHeader, message);
            var value = existingHeader.FirstOrDefault().FirstOrDefault();
            Assert.IsNotNullOrEmpty(value);
        }

        public static void SuccessOK(BaseResponse result, string message = null)
        {
            Success(result, message);
            Ok(result, message);
        }

        public static void Success(BaseResponse result, string message = null)
        {
            if (message == null) message = string.Format("Failed:{0}", result.Message);
            Assert.IsTrue(result.Success, message);
            
        }
        public static void IsStatusCode(BaseResponse result, StatusCode code, string message = null)
        {
            if (message == null) message = string.Format("Failed:{0}", result.Message);
            Assert.AreEqual(code, result.StatusCode, message);
        }

        public static void Ok(BaseResponse result, string message = null)
        {
            if (message == null) message = string.Format("Failed:{0}", result.Message);
            Assert.AreEqual(StatusCode.OK, result.StatusCode, message);
        }

        public static void Fail(BaseResponse result, string message = null)
        {
            if (message == null) message = string.Format("Failed:{0}", result.Message);
            Assert.IsFalse(result.Success, message);
        }
        public static void NotFound(BaseResponse result, string message = null)
        {
            if (message == null) message = string.Format("Failed:{0}", "User should not have been able to do this!. Message: " + result.Message);
            Assert.AreEqual(StatusCode.NotFound, result.StatusCode, message);
        }
        public static void Unauthorized(BaseResponse result, string message = null)
        {
            if (message == null) message = string.Format("Failed:{0}", "User should not have been able to do this!. Message: " + result.Message);
            Assert.AreEqual(StatusCode.Unauthorized, result.StatusCode, message);
        }
        public static void Forbidden(BaseResponse result, string message = null)
        {
            if (message == null) message = string.Format("Failed:{0}", "User should not have been able to do this!. Message: " + result.Message);
            Assert.AreEqual(StatusCode.Forbidden, result.StatusCode, message);
        }

        public static void BadRequest(BaseResponse result, string message = null)
        {
            if (message == null) message = string.Format("Failed:{0}", "User should not have been able to do this!. Message: " + result.Message);
            Assert.AreEqual(StatusCode.BadRequest, result.StatusCode, message);
        }

    }
}
