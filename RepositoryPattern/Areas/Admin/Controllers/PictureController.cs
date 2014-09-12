using RepositoryPattern.Controllers;
using RepositoryPattern.Model.Media;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPattern.Areas.Admin.Controllers
{
    public class PictureController : BaseController
    {
        //
        // GET: /Admin/Picture/
        private readonly IPictureRepository _pictureRepository;
        public PictureController(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }
        public string Paths { get; set; }
        public HttpPostedFileBase TFile { get; set; }
        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            Picture picture = new Picture();
            foreach (string fileName in Request.Files)
            {
                //HttpPostedFileBase file = Request.Files[fileName];
                TFile = Request.Files[fileName];
                //Save file content goes here
                fName = TFile.FileName;
                if (TFile != null && TFile.ContentLength > 0)
                {
                   
                    var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages", Server.MapPath(@"\")));

                    string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                    var fileName1 = Path.GetFileName(TFile.FileName);
                  

                    bool isExists = System.IO.Directory.Exists(pathString);

                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);

                    var path = string.Format("{0}\\{1}", pathString, TFile.FileName);
                    Paths = path;
                    TFile.SaveAs(path);

                }

            }

           
            Stream stream = TFile.InputStream;
            var FileBinary = new byte[stream.Length/1000];
            //EncoderParameter param = new EncoderParameter(Encoder.Quality, FileBinary);
            //Image img = Image.FromStream(stream);
            //var ss = img.Co

            picture.SeoFileName = null;
            picture.MimeType = TFile.ContentType;
            picture.IsNew = true;
            picture.FilePath = Path.GetFileName(TFile.FileName);
            picture.PictureBinary = FileBinary;
            _pictureRepository.Insert(picture);

            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
        

    }
}
