using LaboratorAnalize.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LaboratorAnalize.Models
{
    public class ProgramareTipAnalizePageModel : PageModel
    {
        public List<AssignedTipAnalizaData> AssignedTipAnalizaDataList = new();

        public void PopulateAssignedTipAnalizaData(LaboratorAnalizeContext context, Programare programare)
        {
            var all = context.TipAnaliza;
            var assigned = new HashSet<int>((programare.ProgramareTipAnalize ?? new List<ProgramareTipAnaliza>()).Select(pt => pt.TipAnalizaID));

            AssignedTipAnalizaDataList = new List<AssignedTipAnalizaData>();
            foreach (var a in all)
            {
                AssignedTipAnalizaDataList.Add(new AssignedTipAnalizaData
                {
                    TipAnalizaID = a.ID,
                    Name = a.Denumire,
                    Assigned = assigned.Contains(a.ID)
                });
            }
        }

        public void UpdateProgramareTipAnalize(LaboratorAnalizeContext context, string[] selectedTipuri, Programare programareToUpdate)
        {
            if (selectedTipuri == null)
            {
                programareToUpdate.ProgramareTipAnalize = new List<ProgramareTipAnaliza>();
                return;
            }

            var selectedHS = new HashSet<string>(selectedTipuri);
            var current = new HashSet<int>(programareToUpdate.ProgramareTipAnalize.Select(pt => pt.TipAnalizaID));

            foreach (var a in context.TipAnaliza)
            {
                if (selectedHS.Contains(a.ID.ToString()))
                {
                    if (!current.Contains(a.ID))
                    {
                        programareToUpdate.ProgramareTipAnalize.Add(new ProgramareTipAnaliza
                        {
                            ProgramareID = programareToUpdate.ID,
                            TipAnalizaID = a.ID
                        });
                    }
                }
                else
                {
                    if (current.Contains(a.ID))
                    {
                        var toRemove = programareToUpdate.ProgramareTipAnalize
                            .SingleOrDefault(i => i.TipAnalizaID == a.ID);
                        if (toRemove != null) context.Remove(toRemove);
                    }
                }
            }
        }
    }
}
