using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class FloorballRESTClient : RestClient
    {
        private RestRequest request;

        public FloorballRESTClient(string url) : base(url)
        {

        }

        public IRestResponse ExecuteRequest(string path, Method method, Dictionary<string, string> urlParams = null, Dictionary<string, string> queryParams = null, object body = null, Dictionary<string,string> headers = null)
        {
            request = new RestRequest(path,method);
            if (urlParams != null)
            {
                foreach (var urlParam in urlParams)
                {
                    request.AddUrlSegment(urlParam.Key, urlParam.Value);
                }
            }
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            switch (method)
            {
                case Method.GET:
                    return ExecuteGET(urlParams);
                case Method.POST:
                    return ExecutePOST(body);
                case Method.PUT:
                    return null;
                case Method.DELETE:
                    return ExecuteDELETE();
                default:
                    return null;
            }
        }

        private IRestResponse ExecuteGET(Dictionary<string,string> queryParams)
        {
            if (queryParams != null)
            {
                foreach (var queryParam in queryParams)
                {
                    request.AddQueryParameter(queryParam.Key, queryParam.Value);
                }
            }
           
            return ExecuteAsGet(request, "GET");
        }

        private IRestResponse ExecutePOST(object body)
        {
            if (body != null)
            {
                //request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(body);
            }

            return ExecuteAsPost(request, "POST");
        }

        private IRestResponse ExecuteDELETE()
        {
            return Execute(request);
        }

    }
}
