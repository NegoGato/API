using Arquitetura.Web.Helpers;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using WebApi.OutputCache.V2;
using WebApiContta.Data;
using WebApiContta.Domain.Aggregate.CertificadoAgg;
using WebApiContta.Domain.Aggregate.XmlNfeAgg;
using WebApiContta.Help;

namespace WebApiContta.Controllers
{
   [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class XmlNfeController : ApiController
    {
        //[DeflateCompression]
        [ActionName("BuscarXmlPorChave")]
        [HttpGet]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public XmlNfe GetXmlProChave([FromUri]string chave)
        {
            try
            {
                var autenticaUsuario = new UsuarioRepository();
                //if (autenticaUsuario.Autentica(usuario))
                //{
                    XmlNfe xmlNfe = MongoRepository<XmlNfe>
                               .Instance
                               .Find(
                                  Query.EQ("Chave", chave)
                               ).FirstOrDefault();

                    return xmlNfe;
                //}
                //else
                //    throw new Exception("Usuário não encontrado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[DeflateCompression]
        [ActionName("BuscarInfEmpresaMes")]
        [HttpGet]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public string GetInfoEmpresaMes([FromUri]int ano, [FromUri]int mes, [FromUri]string cnpj)
        {
            try
            {
                var autenticaUsuario = new UsuarioRepository();
                var quantidadeCte = 0;
                var quantidadeECF = 0;
                var quantidadeNFS = 0;
                //if (autenticaUsuario.Autentica(usuario))
                //{
                var quantidadeNfe = MongoRepository<XmlNfe>
                           .Instance
                           .Find(
                                 Query.And(
                                 Query.Or(Query.EQ("CnpjEmitente", cnpj),
                                          Query.EQ("CnpjDest", cnpj)),                                                                
                                 Query.EQ("AnoEmissao", ano),
                                 Query.EQ("MesEmissao", mes),
                                 Query.EQ("Tipo", "NFE")

                           )).Count();
                string retorno = "NF-e - "+ quantidadeNfe+"|\n"+
                                 "CT-e - "+quantidadeCte+"|\n"+
                                 "ECF  - "+quantidadeECF+"|\n"+
                                 "NFS-e- "+quantidadeNFS;
                return retorno;
                //return  Json(new{success= retorno});
                ////}
                //else
                //    throw new Exception("Usuário não encontrado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[DeflateCompression]
        [ActionName("BuscarInfEmpresaAno")]
        [HttpGet]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public string GetInfoEmpresaAno([FromUri]string cnpj)
        {
            try
            {
                var autenticaUsuario = new UsuarioRepository();
                var quantidadeCte = 0;
                var quantidadeECF = 0;
                var quantidadeNFS = 0;
                //if (autenticaUsuario.Autentica(usuario))
                //{
                var quantidadeNfe = MongoRepository<XmlNfe>
                           .Instance
                           .Find(
                                 Query.And(
                                 Query.Or(Query.EQ("CnpjEmitente", cnpj),
                                          Query.EQ("CnpjDest", cnpj)),
                                          Query.EQ("Tipo", "NFE")

                           )).Count();
                string retorno = "NF-e - " + quantidadeNfe + "|\n" +
                                 "CT-e - " + quantidadeCte + "|\n" +
                                 "ECF  - " + quantidadeECF + "|\n" +
                                 "NFS-e- " + quantidadeNFS;
                return retorno;
                //}
                //else
                //    throw new Exception("Usuário não encontrado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[DeflateCompression]
        //[ActionName("BuscarCertificado")]
        //[HttpGet]
        //[CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        //public Certificado GetCertificado([FromUri]string cnpj)
        //{
        //    try
        //    {
        //        var autenticaUsuario = new UsuarioRepository();
        //        //if (autenticaUsuario.Autentica(usuario))
        //        //{
        //        var Certificado = MongoRepository<Certificado>
        //                   .Instance
        //                   .Find(
        //                         Query.EQ("Cnpj", cnpj)
        //                                 ).SingleOrDefault();

                
        //        return Certificado;
        //        //}
        //        //else
        //        //    throw new Exception("Usuário não encontrado");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[DeflateCompression]
        [ActionName("BuscarProc")]
        [HttpGet]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public List<Proc> GetProc([FromUri]string chave)
        {
            try
            {
                var autenticaUsuario = new UsuarioRepository();
                //if (autenticaUsuario.Autentica(usuario))
                //{

                List<Proc> proc = MongoRepository<Proc>
                           .Instance
                           .Find(
                              Query.EQ("ChaveNfe", chave)
                           ).ToList();

                return proc;
                //}
                //else
                //    throw new Exception("Usuário não encontrado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[DeflateCompression]
        [ActionName("SalvarXmlNfe")]
        [HttpPost]
        public XmlNfe PostGravarXml([FromBody] XmlNfe xml)
        {
            try
            {

                XmlNfe xmlNfe = new XmlNfe();

                XmlNfe VerificarNfeExiste = MongoRepository<XmlNfe>
                              .Instance
                              .Find(
                                 Query.And(
                                 Query.EQ("Chave", xml.Chave)
                                 )).FirstOrDefault();

                if (VerificarNfeExiste==null)
                {
                    MongoRepository<XmlNfe>.Instance.Save(xml);

                    xmlNfe = MongoRepository<XmlNfe>
                               .Instance
                               .Find(
                                  Query.EQ("Chave", xml.Chave)
                               ).FirstOrDefault();

                }
                               

                return xmlNfe;
                
                //var autenticaUsuario = new UsuarioRepository();
                //if (autenticaUsuario.Autentica(usuario))
                //{
                //}
                //else
                //    throw new Exception("Usuário não encontrado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ActionName("SalvarCertificado")]
        [HttpPost]
        public string PostGravarCertificado([FromBody] Certificado certificado)
        {
            try
            {
                               
                Certificado VerificarCertificadoExiste = MongoRepository<Certificado>
                              .Instance
                              .Find(
                                 Query.And(
                                 Query.EQ("Cnpj", certificado.Cnpj)
                                 )).FirstOrDefault();

                if (VerificarCertificadoExiste == null)
                {
                    MongoRepository<Certificado>.Instance.Save(certificado);
                    return "Ok";                    

                }
                
                return "Certificado já está cadastrado";
                                
            }
           catch (Exception ex)
            {
                throw ex;
            }
        }

        [ActionName("BuscarCertificadoOne")]
        [HttpGet]
        public Certificado PostBuscarCertificadoOne([FromUri] string cnpj)
        {
            try
            {

                Certificado BuscarCertificado = MongoRepository<Certificado>
                              .Instance
                              .Find(
                                 Query.And(
                                 Query.EQ("Cnpj", cnpj)
                                 )).FirstOrDefault();

                return BuscarCertificado;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ActionName("BuscarCertificados")]
        [HttpGet]
        public List<Certificado> PostBuscarCertificado()
        {
            try
            {

                var BuscarCertificado = MongoRepository<Certificado>
                              .Instance
                              .FindAll().ToList();

                return BuscarCertificado;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ActionName("SalvarProc")]
        [HttpPost]
        public Proc PostGravarProc([FromBody] Proc proc)
        {
            try
            {

                Proc obsProc = new Proc();

                Proc VerificarProtocoloExiste = MongoRepository<Proc>
                              .Instance
                              .Find(
                                 Query.And(
                                 Query.EQ("ChaveNfe", proc.ChaveNfe),
                                 Query.EQ("IdEvento", proc.IdEvento)
                                 )).FirstOrDefault();

                if (VerificarProtocoloExiste==null)
                {
                    MongoRepository<Proc>.Instance.Save(proc);

                    obsProc = MongoRepository<Proc>
                                   .Instance
                                   .Find(
                                      Query.EQ("ChaveNfe", proc.ChaveNfe)
                                   ).FirstOrDefault();
                                       
                }

                

                return obsProc;
                

                //var autenticaUsuario = new UsuarioRepository();
                //if (autenticaUsuario.Autentica(usuario))
                //{
                //}
                //else
                //    throw new Exception("Usuário não encontrado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //[DeflateCompression]
        [ActionName("ApiOnline")]
        [HttpGet]
        public string ApiOnline()
        {
            try
            {
                return "Api Online.";

                //var autenticaUsuario = new UsuarioRepository();
                //if (autenticaUsuario.Autentica(usuario))
                //{
                //}
                //else
                //    throw new Exception("Usuário não encontrado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[DeflateCompression]
        //[ActionName("UploadDocumentoNovo")]
        //[HttpPost]
        //public Task<IEnumerable<FileDesc>> Post()
        //{
        //    var folderName = "Upload";
        //    var PATH = HttpContext.Current.Server.MapPath("~/" + folderName);
        //    var rootUrl = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, String.Empty);
        //    if (Request.Content.IsMimeMultipartContent())
        //    {
        //        try
        //        {
        //            var streamProvider = new CustomMultipartFormDataStreamProvider(PATH);
        //            var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith<IEnumerable<FileDesc>>(t =>
        //            {
        //                if (t.IsFaulted || t.IsCanceled)
        //                {
        //                    throw new HttpResponseException(HttpStatusCode.InternalServerError);
        //                }

        //// This illustrates how to get the file names.
        //foreach (MultipartFileData file in provider.FileData)
        //{
        //    var nome = file.Headers.ContentDisposition.FileNameStar;

        //    }
        //    else
        //    {
        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
        //    }
        //}
        //[DeflateCompression]
        //[ActionName("UploadDocumentoNovo")]
        //[HttpPost]
        //public Task<IEnumerable<FileDesc>> PostUploadDocumentoNovo()
        //{
        //    var folderName = "Upload";
        //    var PATH = HttpContext.Current.Server.MapPath("~/" + folderName);
        //    var rootUrl = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, String.Empty);
        //    if (Request.Content.IsMimeMultipartContent())
        //    {
        //        try
        //        {
        //            var streamProvider = new CustomMultipartFormDataStreamProvider(PATH);
        //            var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith<IEnumerable<FileDesc>>(t =>
        //            {
        //                if (t.IsFaulted || t.IsCanceled)
        //                {
        //                    throw new HttpResponseException(HttpStatusCode.InternalServerError);
        //                }

        //                var fileInfo = streamProvider.FileData.Select(i =>
        //                {
        //                    var info = new FileInfo(i.LocalFileName);
        //                    return new FileDesc(info.Name, rootUrl + "/" + folderName + "/" + info.Name, info.Length / 1024);
        //                });
        //                return fileInfo;
        //            });

        //            return task;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    else
        //    {
        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
        //    }
        //}

        //[DeflateCompression]
        [ActionName("UploadDocumento")]
        [HttpPost]
        public async Task<object> UploadFileUploadDocumentoNovo1()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException
                 (Request.CreateResponse(HttpStatusCode.UnsupportedMediaType));
            }
            MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(
              HttpContext.Current.Server.MapPath("~/Upload"));
            await Request.Content.ReadAsMultipartAsync(streamProvider);

            foreach (MultipartFileData fileData in streamProvider.FileData)
            {
                string fileName = "";
                if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                {
                    fileName = Guid.NewGuid().ToString();
                }
                fileName = fileData.Headers.ContentDisposition.FileName;
                if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                {
                    fileName = fileName.Trim('"');
                }
                if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                {
                    fileName = Path.GetFileName(fileName);
                }
                File.Move(fileData.LocalFileName,
                  Path.Combine(HttpContext.Current.Server.MapPath("~/Upload/"), fileName));
            }

            return new
            {
                FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName),
            };
        }

        [ActionName("DownloadDocumento")]
        [HttpGet]
        public HttpResponseMessage Download()
        {
            string diretorio = @"E:\Projetos\WebApiContta\WebApiContta\Upload";//Caminho do diretório 
            string arquivo = @"E:\Projetos\WebApiContta\WebApiContta\Download\Download.zip";//Caminho do arquivo zip a ser criado 
            ZipFile.CreateFromDirectory(diretorio, arquivo);
            string diretorio1 = @"E:\Projetos\WebApiContta\WebApiContta\Upload";

            DirectoryInfo directorySelected = new DirectoryInfo(diretorio);
            DirectoryInfo directorySelected1 = new DirectoryInfo(diretorio1);
            foreach (FileInfo fileToCompress in directorySelected.GetFiles())
            {

                foreach (var fileToCompress1 in directorySelected1.GetFiles()){

                     if(fileToCompress.Name == fileToCompress1.Name)
                     {
                         fileToCompress1.Delete();
                         break;
                     }

                }
	            
            }
            string filePath = @"E:\Projetos\WebApiContta\WebApiContta\Download\Download.zip";
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var response = new FileHttpResponseMessage(filePath); 
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Download.zip"
            }; 
            return response; 



            //HttpResponseMessage result = null;
            //var localFilePath = HttpContext.Current.Server.MapPath("~/Download/Download.zip");

            //if (!File.Exists(localFilePath))
            //{
            //    result = Request.CreateResponse(HttpStatusCode.Gone);
            //    System.IO.File.Delete(localFilePath);
            //}
            //else
            //{
            //    // Serve the file to the client
            //    result = Request.CreateResponse(HttpStatusCode.OK);
            //    result.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
            //    result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            //    result.Content.Headers.ContentDisposition.FileName = "Download.zip";
              
            //    System.IO.File.Delete(localFilePath);
            //}



            //return result;
                     
            
        }

        [ActionName("DownloadXml")]
        [HttpGet]
        public List<XmlNfe> Download([FromUri] string mes, [FromUri] string ano, [FromUri] string numeroChave, [FromUri] bool cancelada, [FromUri] string cnpj, [FromUri] string metodoUsar)
        {
            
            List<XmlNfe> listaXml;
            int testemes = int.Parse(mes);
            int testeano = int.Parse(ano);

            if (metodoUsar.Equals("TodasNotas"))
            {
                listaXml = MongoRepository<XmlNfe>
                           .Instance
                           .Find(
                               Query.And(
                               Query.Or(Query.EQ("CnpjEmitente", cnpj),
                                        Query.EQ("CnpjDest", cnpj))
                           )).ToList();

                return listaXml;               

            }
            else if (metodoUsar.Equals("TodasNotasDoAno"))
            {
                listaXml = MongoRepository<XmlNfe>
                             .Instance
                             .Find(
                                 Query.And(
                                 Query.Or(Query.EQ("CnpjEmitente", cnpj),
                                          Query.EQ("CnpjDest", cnpj)),
                                          Query.EQ("AnoEmissao", testeano)
                             )).ToList();

                return listaXml;
            }
            else if (metodoUsar.Equals("TodasNotasDoMes"))
            {
                listaXml = MongoRepository<XmlNfe>
                             .Instance
                             .Find(
                                 Query.And(
                                 Query.Or(Query.EQ("CnpjEmitente", cnpj),
                                          Query.EQ("CnpjDest", cnpj)),
                                          Query.EQ("MesEmissao", testemes)
                             )).ToList();

                return listaXml;
            }
            else if (metodoUsar.Equals("BaixarNotasPeriodo"))
            {
               
                 listaXml = MongoRepository<XmlNfe>
                             .Instance
                             .Find(
                                 Query.And(
                                 Query.Or(Query.EQ("CnpjEmitente", cnpj),
                                          Query.EQ("CnpjDest", cnpj)),
                                          Query.EQ("MesEmissao", testemes),
                                          Query.EQ("AnoEmissao", testeano)
                             )).ToList();

                return listaXml;
            }


            //Canceladas

            else if (metodoUsar.Equals("TodasNotasCanceldas"))
            {
                listaXml = MongoRepository<XmlNfe>
                           .Instance
                           .Find(
                               Query.And(
                               Query.Or(Query.EQ("CnpjEmitente", cnpj),
                                        Query.EQ("CnpjDest", cnpj)),
                                        Query.EQ("IsCancelada", cancelada)
                           )).ToList();

                return listaXml;

            }
            else if (metodoUsar.Equals("TodasNotasDoAnoCanceladas"))
            {
                 listaXml = MongoRepository<XmlNfe>
                             .Instance
                             .Find(
                                 Query.And(
                                 Query.Or(Query.EQ("CnpjEmitente", cnpj),
                                          Query.EQ("CnpjDest", cnpj)),
                                          Query.EQ("AnoEmissao", testeano),
                                          Query.EQ("IsCancelada", cancelada)
                             )).ToList();

                return listaXml;
            }
            else if (metodoUsar.Equals("TodasNotasDoMesCanceladas"))
            {
                listaXml = MongoRepository<XmlNfe>
                             .Instance
                             .Find(
                                 Query.And(
                                 Query.Or(Query.EQ("CnpjEmitente", cnpj),
                                          Query.EQ("CnpjDest", cnpj)),
                                          Query.EQ("MesEmissao", testemes),
                                          Query.EQ("IsCancelada", cancelada)
                             )).ToList();

                return listaXml;
            }
            else if (metodoUsar.Equals("BaixarNotasPeriodoCanceladas"))
            {
                listaXml = MongoRepository<XmlNfe>
                             .Instance
                             .Find(
                                 Query.And(
                                 Query.Or(Query.EQ("CnpjEmitente", cnpj),
                                          Query.EQ("CnpjDest", cnpj)),
                                          Query.EQ("MesEmissao", testemes),
                                          Query.EQ("AnoEmissao", testeano),
                                          Query.EQ("IsCancelada", cancelada)
                             )).ToList();

                return listaXml;
            }

            //Chave ou Numero

            else if (metodoUsar.Equals("BaixarNotaChaveNumero"))
            {
               listaXml = MongoRepository<XmlNfe>
                             .Instance
                             .Find(
                                 Query.And(
                                 Query.Or(Query.EQ("CnpjEmitente", cnpj),
                                          Query.EQ("CnpjDest", cnpj)),
                                          Query.EQ("MesEmissao", testemes),
                                          Query.EQ("AnoEmissao", testeano),
                                          Query.EQ("IsCancelada", cancelada)
                             )).ToList();

                return listaXml;
            }
           

                return listaXml = new List<XmlNfe>();
            
                       
       }




        //public void Download()
        //{
        //    //foreach (var file in DirSearch("~/Upload"))//Directory.EnumerateFiles(CaminhoEntrada, "*.xml", SearchOption.AllDirectories))
        //    //{
        //    string path = @"E:\Projetos\WebApiContta\WebApiContta\Upload";
        //    DirectoryInfo directorySelected = new DirectoryInfo(path);

        //    foreach (FileInfo fileToCompress in directorySelected.GetFiles())
        //        {

        //            using (FileStream originalFileStream = fileToCompress.OpenRead())
        //            {
        //                if ((File.GetAttributes(fileToCompress.FullName) &
        //                   FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
        //                {
        //                    using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ".gz"))
        //                    {
        //                        using (GZipStream compressionStream = new GZipStream(compressedFileStream,
        //                           CompressionMode.Compress))
        //                        {
        //                            originalFileStream.CopyTo(compressionStream);

        //                        }
        //                    }
        //                    FileInfo info = new FileInfo(path + "\\" + fileToCompress.Name + ".gz");
        //                    //Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
        //                    //fileToCompress.Name, fileToCompress.Length.ToString(), info.Length.ToString());

        //                }

        //            }
        //        }         


        //    }




        private List<String> DirSearch(string sDir)
        {
            var filePaths = Directory.EnumerateFiles(sDir, "*.*").ToList();

            for (int i = 0; i < filePaths.Count; i++)
            {
                char[] separador = { '.' };
                var naoEhXml = filePaths[i].Split(separador);

                if (!naoEhXml[1].Equals("xml"))
                {
                    filePaths.RemoveAt(i);
                }
            }

            return filePaths;
        }

        //    try
        //    {
        //        var provider = new MultipartMemoryStreamProvider();
        //        await Request.Content.ReadAsMultipartAsync(provider);
        //        foreach (var file in provider.Contents)
        //        {
        //            var dataStream = await file.ReadAsStreamAsync();
        //            // use the data stream to persist the data to the server (file system etc)

        //            var response = Request.CreateResponse(HttpStatusCode.OK);
        //            response.Content = new StringContent("Successful upload", Encoding.UTF8, "text/plain");
        //            response.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue(@"text/html");
        //            return response;
                
        //         }
        //    }
        //    catch (Exception e)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}
                       
        //public Task<HttpResponseMessage> PostFile()
        // {
        //    try
        //    {
        //HttpRequestMessage request = this.Request;
        //if (!request.Content.IsMimeMultipartContent())
        //{
        //    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //}

        //string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/uploads");
        //var provider = new MultipartFormDataStreamProvider(root);

        //var task = request.Content.ReadAsMultipartAsync(provider).
        //    ContinueWith<HttpResponseMessage>(o =>
        //    {
        //        string file1 = provider.FormData.GetValues.First().Value;
        //        // this is the file name on the server where the file was saved
       
        //        return new HttpResponseMessage()
        //        {
        //            Content = new StringContent("File uploaded.")
        //        };
        //    }
        //);
        //return task;
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        //}
    }
}
