using Xceed.Words.NET;
using Common;


namespace MailSenderWPF.Services
{
    class Reporting
    {
        EmailxmlContainer db = new EmailxmlContainer();
        public Reporting()
        {
            using (DocX doc = DocX.Load("report.doc"))
            {
                Paragraph paragraph = doc.InsertParagraph();
                if(db.Emails!=null)
                {
                    foreach (var el in db.Emails)
                    {
                        paragraph.Append($"ID: {el.Id}\tEmail: {el.Value}\tName: {el.Name}\n");
                    }
                    doc.Save();
                }
            }
        }
    }
}
