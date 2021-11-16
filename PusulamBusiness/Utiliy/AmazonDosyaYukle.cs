using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.IO;

namespace PusulamBusiness.Utility
{
    public class AmazonDosyaYukle
    {

        private static readonly string bucketName = "okyanusdata";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.EUWest1;

        public static bool sendMyFileToS3(string subDirectoryInBucket, string fileNameInS3, Stream fileToUpload, string contentType, string TCKIMLIKNO)
        {
            try
            {
                var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("AKIAIEI6COBYD724X73Q", "sjQNd7wWA22DqimEtt0KUYbGJAnTYH7ViIq8lskM");

                IAmazonS3 client = new AmazonS3Client(awsCredentials, bucketRegion);

                // create a TransferUtility instance passing it the IAmazonS3 created in the first step
                TransferUtility utility = new TransferUtility(client);
                // making a TransferUtilityUploadRequest instance
                TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

                if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
                {
                    request.BucketName = bucketName; //no subdirectory just bucket name
                }
                else
                {   // subdirectory and bucket name
                    request.BucketName = bucketName + @"/" + subDirectoryInBucket;
                }
                request.Key = fileNameInS3; //file name up in S3
                                            //request.FilePath = localFilePath; //local file name
                request.InputStream = fileToUpload;
                request.ContentType = contentType;
                request.CannedACL = S3CannedACL.PublicRead;
                request.Metadata.Add("TCKIMLIKNO", TCKIMLIKNO);
                utility.Upload(request); //commensing the transfer

                return true; //indicate that the file was sent
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}