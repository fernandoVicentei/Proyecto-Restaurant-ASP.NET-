using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Proyecto1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using iText.Kernel.Events;
namespace Proyecto1.Controllers
{
    public class reporteController : Controller
    {
        ventas ventas = new ventas();
        // GET: reporte
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult detalles()
        {
            //obtenemos ubicacion de memoria
            MemoryStream memo = new MemoryStream();
            PdfWriter pp = new PdfWriter(memo);
            PdfDocument doc = new PdfDocument(pp);
            //el tipo de hoja
            Document documento = new Document(doc, PageSize.LETTER);
            documento.SetMargins(75, 35, 70, 35);

            // para un tipo de letra personalizado
            string font = Server.MapPath("/fonts/Megan tatatatat ruta del tipo de letra");
            PdfFont fond = PdfFontFactory.CreateFont(FontConstants.HELVETICA);
            //creamos stilos personalizados
            Style estilo = new Style().SetFontSize(24).SetFont(fond).SetFontColor(ColorConstants.BLACK)
                                       .SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.CENTER);

            documento.Add(new Paragraph(" FERANVI - FOOT").AddStyle(estilo));
            /*documento.Add(new Paragraph(" Hellow Wordl")
                .SetFontColor(ColorConstants.BLUE)
                .SetFont(fond).SetFontSize(24));*/

            //damos estilo a la celda

            Style stylecell = new Style()
            .SetBackgroundColor(ColorConstants.WHITE)
            .SetFontColor(ColorConstants.BLACK)           
            .SetTextAlignment(TextAlignment.CENTER);

            //retornamos ventas
            var v = ventas.Detallesbol();

            //para generar una tabla

            Table _tabla = new Table(6).UseAllAvailableWidth();
            Cell _cell = new Cell(2, 1).Add(new Paragraph("ID"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell(1, 5).Add(new Paragraph("DETALLES BOLETAS"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell().Add(new Paragraph("ID Pedido"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));         
            _cell = new Cell().Add(new Paragraph("Modo Pagos"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell().Add(new Paragraph("Observacion"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell().Add(new Paragraph("Fecha"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell().Add(new Paragraph("Monto Final"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));

            int aux = 0;
            foreach (var item in v)
            {
                _cell = new Cell().Add(new Paragraph(item.id.ToString()));
                _tabla.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(item.idpedido.ToString()));
                _tabla.AddCell(_cell);                

                if (item.idmodopago == 7)
                {
                    _cell = new Cell().Add(new Paragraph("PERSONAL"));
                    _tabla.AddCell(_cell.SetBackgroundColor(ColorConstants.BLUE).SetFontColor(ColorConstants.WHITE).SetTextAlignment(TextAlignment.CENTER));
                }
                else if(item.idmodopago == 8)
                {
                    _cell = new Cell().Add(new Paragraph("PAYPAL"));
                    _tabla.AddCell(_cell.SetBackgroundColor(ColorConstants.RED).SetFontColor(ColorConstants.WHITE).SetTextAlignment(TextAlignment.CENTER));
                }
                else
                {
                    _cell = new Cell().Add(new Paragraph("NINGUNO"));
                    _tabla.AddCell(_cell.SetBackgroundColor(ColorConstants.BLACK).SetFontColor(ColorConstants.WHITE).SetTextAlignment(TextAlignment.CENTER));
                }

                _cell = new Cell().Add(new Paragraph(item.observacion));
                _tabla.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(item.fecha.ToString()));
                _tabla.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(item.montifinal.ToString()+" bs" ));
                _tabla.AddCell(_cell);
            }
            documento.Add(_tabla);
            documento.Add(new Paragraph("Fecha de Reporte : " + DateTime.Now));
            documento.Close();
            byte[] bytestream = memo.ToArray();
            memo = new MemoryStream();
            memo.Write(bytestream, 0, bytestream.Length);
            memo.Position = 0;
            return new FileStreamResult(memo, "application/pdf");
        }
        public ActionResult PDF()
        {
            //obtenemos ubicacion de memoria
            MemoryStream memo = new MemoryStream();
            PdfWriter pp = new PdfWriter(memo);
            PdfDocument doc = new PdfDocument(pp);            
            //el tipo de hoja
            Document documento = new Document(doc, PageSize.LETTER);

            documento.SetMargins(75,35,70,35);

            // para un tipo de letra personalizado
            string font = Server.MapPath("/fonts/Megan tatatatat ruta del tipo de letra");
            PdfFont fond = PdfFontFactory.CreateFont(FontConstants.HELVETICA);
            //creamos stilos personalizados
            Style estilo = new Style().SetFontSize(24).SetFont(fond).SetFontColor(ColorConstants.BLACK)
                                       .SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.CENTER);

            documento.Add(new Paragraph(" FERANVI - FOOT").AddStyle(estilo));
            /*documento.Add(new Paragraph(" Hellow Wordl")
                .SetFontColor(ColorConstants.BLUE)
                .SetFont(fond).SetFontSize(24));*/

            //damos estilo a la celda

            Style stylecell = new Style()
            .SetBackgroundColor(ColorConstants.BLACK)
            .SetFontColor(ColorConstants.WHITE)
            .SetTextAlignment(TextAlignment.CENTER);

            //retornamos ventas
            var v = ventas.Retornoventas();

            //para generar una tabla

            Table _tabla = new Table(10).UseAllAvailableWidth();
            Cell _cell = new Cell(2, 1).Add(new Paragraph("ID"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell(1, 9).Add(new Paragraph("REPORTES PEDIDOS"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell().Add(new Paragraph("Nombre Producto"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell().Add(new Paragraph("Cliente"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));            
            _cell = new Cell().Add(new Paragraph("Observacion"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell().Add(new Paragraph("Cant"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell().Add(new Paragraph("TipoPedido"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell().Add(new Paragraph("PrecioUnitario"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell().Add(new Paragraph("Fecha"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell().Add(new Paragraph("Detalle Pedido"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));
            _cell = new Cell().Add(new Paragraph("Monto Final"));
            _tabla.AddHeaderCell(_cell.AddStyle(stylecell));        

            int aux = 0;
            foreach(var item in v)
            {
                _cell = new Cell().Add(new Paragraph(item.idpedido.ToString()));
                _tabla.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(item.nombre));
                _tabla.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(item.idcliente.ToString()));
                _tabla.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(item.observacion));
                _tabla.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(item.cantidad.ToString()));
                _tabla.AddCell(_cell);

                if (item.tipopedido==1)
                {
                    _cell = new Cell().Add(new Paragraph("NORMAL"));
                    _tabla.AddCell(_cell.SetBackgroundColor(ColorConstants.BLUE).SetFontColor(ColorConstants.WHITE).SetTextAlignment(TextAlignment.CENTER));                   
                }
                else
                {
                    _cell = new Cell().Add(new Paragraph("PROMOCION"));
                    _tabla.AddCell(_cell.SetBackgroundColor(ColorConstants.RED).SetFontColor(ColorConstants.WHITE).SetTextAlignment(TextAlignment.CENTER));
                }

                _cell = new Cell().Add(new Paragraph(item.precio.ToString()+" bs."));
                _tabla.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(item.fecha.ToString()));
                _tabla.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(item.detalle));
                _tabla.AddCell(_cell);
                _cell = new Cell().Add(new Paragraph(item.montifinal.ToString() + " bs."));
                _tabla.AddCell(_cell);
            }
            documento.Add(_tabla);
            documento.Add(new Paragraph("Fecha de Reporte : " +DateTime.Now ));
            documento.Close();
            byte[] bytestream = memo.ToArray();
            memo = new MemoryStream();
            memo.Write(bytestream,0,bytestream.Length  );
            memo.Position = 0;
            return new FileStreamResult(memo, "application/pdf");
          

        }
    }
}