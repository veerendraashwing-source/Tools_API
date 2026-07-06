using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easychit_Api.Security.Authentication
{
    public class AppSettings
    {
        public string Secret { get; set; }
    }
    public class Jwtsecret
    {
        public string Issuer { get; set; }
    }

    public class S3BucketURL
    {
        public string s3bucketAPiurl { get; set; }
    }
}
