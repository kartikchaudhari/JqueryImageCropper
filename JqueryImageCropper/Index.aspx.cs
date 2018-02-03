using System;
using System.Collections.Generic;
//drawing relataed operatoions and rectangle shape 
using System.Drawing;

//file I/O operations
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JqueryImageCropper
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string UploadFileName = "";
            string UploadFilePath = "";

            if (FileUpload1.HasFile)
            {
                string ext = Path.GetExtension(FileUpload1.FileName).ToLower();
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".png")
                {
                    UploadFileName = Guid.NewGuid().ToString() + ext;
                    UploadFilePath = Path.Combine(Server.MapPath("~/ui"), UploadFileName);
                    try
                    {
                        FileUpload1.SaveAs(UploadFilePath);
                        imgUpload.ImageUrl = "~/ui/" + UploadFileName;
                        Panel1.Visible = true;
                    }
                    catch (Exception ex) {
                        throw;
                    }
                }
                else {
                    lblMsg.Text = "Only Image Fi;es are allowed";
                }

            }
            else {
                lblMsg.Text = "Plese Select a File";
            }

        }

        protected void btnCrop_Click(object sender, EventArgs e)
        {
            string fileName = Path.GetFileName(imgUpload.ImageUrl);
            string filePath = Path.Combine(Server.MapPath("~/ui"),fileName);
            string cropFileName = "";
            string cropFilePath = "";
            if (File.Exists(filePath)) 
            {
                System.Drawing.Image orgImge = System.Drawing.Image.FromFile(filePath);
                Rectangle CropArea = new Rectangle(
                        Convert.ToInt32(X.Value),
                        Convert.ToInt32(Y.Value),
                        Convert.ToInt32(W.Value),
                        Convert.ToInt32(H.Value));
                try
                {
                    Bitmap bitMap = new Bitmap(CropArea.Width, CropArea.Height);
                    using (Graphics g = Graphics.FromImage(bitMap)) 
                    {
                        g.DrawImage(orgImge, new Rectangle(0, 0, bitMap.Width, bitMap.Height), CropArea, GraphicsUnit.Pixel);
                    }

                    cropFileName = "crop_" + fileName;
                    cropFilePath = Path.Combine(Server.MapPath("~/ui"), cropFileName);
                    bitMap.Save(cropFilePath);
                    Response.Redirect("~/ui/" + cropFileName, false);
                }

                catch (Exception ex) {
                    throw;
                }
            }
        }
    }
}