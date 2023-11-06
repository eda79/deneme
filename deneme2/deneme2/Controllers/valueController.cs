using deneme2.Models;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf; 
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Extgstate;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace deneme2.Controllers
{

    public class valueController : Controller
    {
        public string encoding = "windows-1254";

        Context _context;
        public  IWebHostEnvironment _env;

        public valueController(IWebHostEnvironment env,Context context)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Pdf()
        {

            MemoryStream ms = new MemoryStream();
            PdfWriter pw = new PdfWriter(ms);
            PdfDocument pdfDocument = new PdfDocument(pw);
            Document doc = new Document(pdfDocument, PageSize.LETTER);
            doc.SetMargins(90, 35, 70, 35);
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA, encoding);

            string PathLogo = System.IO.Path.Combine(_env.WebRootPath, "images", "Adsız (2).png");
            Image img = new Image(ImageDataFactory.Create(PathLogo));
            img.ScaleAbsolute(2000, 1050);

            //string PathLogo2 = Server.MapPath("~/Content/Image/png-papatya2pkb9b.png");
            //Image img2 = new Image(ImageDataFactory.Create(PathLogo2));

            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler(img));
            pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterEventHandler());
            //pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundColorHandler());
            //pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundImageHandler(img2));

            Table table3 = new Table(1).UseAllAvailableWidth();
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            Cell cell3 = new Cell().Add(new Paragraph("Adress").SetFont(bold))
             .Add(new Paragraph("Kirazlı Mah Çamlık Cad. No:41").SetFont(font).SetFontSize(9))
            .Add(new Paragraph("Phone").SetFont(bold))
            .Add(new Paragraph("05555555").SetFontSize(9));
            cell3.SetTextAlignment(TextAlignment.LEFT);
            cell3.SetBorder(Border.NO_BORDER);
            table3.AddCell(cell3);
            doc.Add(table3);

            DeviceRgb _blue = new DeviceRgb(5, 67, 112);
            Table table = new Table(1).UseAllAvailableWidth().SetMarginTop(15f);
            Cell cell = new Cell().Add(new Paragraph("Ürün Listesi").SetFontSize(6).SetFontColor(ColorConstants.WHITE).SetBackgroundColor(_blue));
            cell.SetTextAlignment(TextAlignment.CENTER);
            cell.SetBorder(Border.NO_BORDER);
            table.AddCell(cell);

            //cell = new Cell().Add(new Paragraph("Ürünler").SetBackgroundColor(ColorConstants.RED));
            //cell.SetTextAlignment(TextAlignment.CENTER);
            //cell.SetBorder(Border.NO_BORDER);
            //table.AddCell(cell);

            doc.Add(table);


            Style styleCell = new Style()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(TextAlignment.CENTER);


            Table table2 = new Table(4).UseAllAvailableWidth();
            Cell cell2 = new Cell(2, 1).Add(new Paragraph("#"));
            table2.AddHeaderCell(cell2.AddStyle(styleCell));

            cell2 = new Cell(1, 2).Add(new Paragraph("Ürün Bilgisi"));
            table2.AddHeaderCell(cell2.AddStyle(styleCell).SetBackgroundColor(ColorConstants.LIGHT_GRAY));

            cell2 = new Cell(2, 1).Add(new Paragraph("Tarih").SetTextAlignment(TextAlignment.CENTER));
            table2.AddHeaderCell(cell2.AddStyle(styleCell));

            cell2 = new Cell().Add(new Paragraph("Ürün Adı"));
            table2.AddHeaderCell(cell2.AddStyle(styleCell));

            cell2 = new Cell().Add(new Paragraph("Fiyat"));
            
            table2.AddHeaderCell(cell2.AddStyle(styleCell));


            List<Product> model = _context.Products.ToList();
            //Türkçe karakter kullanımı için

            int x = 0;
            foreach (var item in model)
            {
                x++;
                cell2 = new Cell().Add(new Paragraph(x.ToString())).SetBackgroundColor(ColorConstants.LIGHT_GRAY);
                table2.AddCell(cell2);

                cell2 = new Cell().Add(new Paragraph(item.Name).SetFont(font));
                table2.AddCell(cell2);

                cell2 = new Cell().Add(new Paragraph("$"+item.Price.ToString()));
                table2.AddCell(cell2);

                cell2 = new Cell().Add(new Paragraph(item.CreatedAt.ToString()).SetFont(font));
                table2.AddCell(cell2);

           
            }
            doc.Add(table2);

            Table table4 = new Table(2).SetMarginTop(80f).SetWidth(130f).SetMarginLeft(400f) ;
            Cell cell4 = new Cell().Add(new Paragraph("Toplam:")).SetBorder(Border.NO_BORDER).SetFont(bold);
            table4.AddCell(cell4);
            cell4 = new Cell().Add(new Paragraph("$4000")).SetBorder(Border.NO_BORDER);
            table4.AddCell(cell4);
            cell4 = new Cell().Add(new Paragraph("KDV%:")).SetBorder(Border.NO_BORDER).SetFont(bold);
            table4.AddCell(cell4);
            cell4 = new Cell().Add(new Paragraph("18")).SetBorder(Border.NO_BORDER);
            table4.AddCell(cell4);
            cell4 = new Cell().Add(new Paragraph("Genel Toplam:")).SetBorder(Border.NO_BORDER).SetFont(bold);
            table4.AddCell(cell4);
            cell4 = new Cell().Add(new Paragraph("$4720")).SetBorder(Border.NO_BORDER);
            table4.AddCell(cell4);
            doc.Add(table4);

            //string nameFont = Server.MapPath("/fonts/KayPhoDu-SemiBold.ttf");
            //PdfFont font = PdfFontFactory.CreateFont(nameFont);

            //Style styles = new Style()
            //    .SetFontSize(24)
            //    .SetFont(font)
            //    .SetFontColor(ColorConstants.BLUE)
            //    .SetBackgroundColor(ColorConstants.RED);

            //doc.Add(new Paragraph("Hello itext7!").AddStyle(styles));
            //    //.SetFontColor(ColorConstants.BLUE)
            //    //.SetFont(font).SetFontSize(24));

            doc.Close();
            byte[] bytesStream = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);
            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");
        }
    }
    public class HeaderEventHandler : IEventHandler
    {
        Image Img;
        public HeaderEventHandler(Image img)
        {
            Img = img;
        }
        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();

            Rectangle rootArea = new Rectangle(35, page.GetPageSize().GetTop() - 70, page.GetPageSize().GetRight() - 70, 50);
            Canvas canvas = new Canvas(page, rootArea);
            canvas
                .Add(getTable(docEvent))
                .ShowTextAligned("Alt Bilgi", 25, 10, TextAlignment.CENTER)
                //.ShowTextAligned("Alt Bilgi", 10, 10, TextAlignment.CENTER)
                .ShowTextAligned("Alt Bilgi", 600, 10, TextAlignment.RIGHT)
                .Close();
        }
        public Table getTable(PdfDocumentEvent docEvent)
        {
            float[] cellwidth = { 20f, 80f };
            Table tableEvent = new Table(UnitValue.CreatePercentArray(cellwidth));

            Style styleCell = new Style()
                .SetBorder(Border.NO_BORDER);

            Style styleText = new Style()
                .SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10f);

            Cell cell = new Cell().Add(Img.SetAutoScale(true));
            tableEvent.AddCell(cell
                .AddStyle(styleCell)
                .SetTextAlignment(TextAlignment.LEFT));
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            cell = new Cell()
                .Add(new Paragraph("Company Name").SetFont(bold))
                .Add(new Paragraph("Departman").SetFont(bold))
                .Add(new Paragraph("Tarih:" +" "+ DateTime.Now.ToShortDateString()).SetFont(bold))
                .AddStyle(styleText).AddStyle(styleCell);
            tableEvent.AddCell(cell);
            return tableEvent;
        }


    }

    public class FooterEventHandler : IEventHandler
    {
        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();
            PdfCanvas pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
            new Canvas(page, new Rectangle(36, 20, page.GetPageSize().GetWidth() - 70, 50))
                //Rectangle rootArea = new Rectangle(35, page.GetPageSize().GetTop() - 70, page.GetPageSize().GetRight() - 70, 50);

                .Add(getTable(docEvent));
        }
        public Table getTable(PdfDocumentEvent docEvent)
        {
            float[] cellWidth = { 92f, 8f };
            Table tableEvent = new Table(UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();

            PdfPage page = docEvent.GetPage();
            int pageNum = docEvent.GetDocument().GetPageNumber(page);

            Style styleCell = new Style()
                .SetPadding(5)
                .SetBorder(Border.NO_BORDER)
                .SetBorderTop(new SolidBorder(ColorConstants.BLACK, 2));

            Cell cell = new Cell().Add(new Paragraph(DateTime.Now.ToLongDateString()));
            tableEvent.AddCell(cell
                .AddStyle(styleCell)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontColor(ColorConstants.LIGHT_GRAY));
            cell = new Cell().Add(new Paragraph(pageNum.ToString()));
            cell.AddStyle(styleCell)
                .SetBackgroundColor(ColorConstants.BLACK)
                .SetFontColor(ColorConstants.WHITE)
                .SetTextAlignment(TextAlignment.CENTER);
            tableEvent.AddCell(cell);
            return tableEvent;
        }
    }

    //public class BackgroundColorHandler : IEventHandler
    //{
    //    Color SolidColor;
    //    public BackgroundColorHandler()
    //    {
    //        SolidColor = new DeviceRgb(0.545f, 0.909f, 0.745f);
    //    }

    //    public void HandleEvent(Event @event)
    //    {
    //        PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
    //        PdfDocument pdf = docEvent.GetDocument();
    //        PdfPage page = docEvent.GetPage();

    //        Rectangle pageSize = page.GetPageSize();
    //        PdfCanvas pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdf);

    //        pdfCanvas.SaveState()
    //            .SetFillColor(SolidColor)
    //            .Rectangle(pageSize.GetLeft(), 0, pageSize.GetWidth(), pageSize.GetHeight())
    //            .Fill().RestoreState();
    //        pdfCanvas.Release();
    //    }
    //}
    //public class BackgroundImageHandler : IEventHandler
    //{
    //    protected PdfExtGState gState;
    //    Image img;
    //    public BackgroundImageHandler(Image _img)
    //    {
    //        gState = new PdfExtGState().SetFillOpacity(0.3f);
    //        this.img = _img;
    //    }

    //    public void HandleEvent(Event @event)
    //    {
    //        PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
    //        PdfDocument pdfDoc = docEvent.GetDocument();
    //        PdfPage page = docEvent.GetPage();
    //        Rectangle pageSize = page.GetPageSize();

    //        PdfCanvas pdfCanvas = new PdfCanvas(page.GetLastContentStream(), page.GetResources(), pdfDoc);
    //        pdfCanvas.SaveState().SetExtGState(gState);

    //        Canvas canvas = new Canvas(page, pageSize);
    //        canvas.Add(img.ScaleAbsolute(pageSize.GetWidth(), pageSize.GetHeight()));
    //        pdfCanvas.RestoreState();
    //        pdfCanvas.Release();

    //    }
    //}
}
