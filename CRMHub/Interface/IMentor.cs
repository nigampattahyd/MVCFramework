using CRMHub.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Interface
{
    public interface IMentor
    {
        Int64 InserMentor(Mentor mentor, string Connectionstring, string dbName);
        Mentor GetMentorDetailsByID(Int64 id, string Connectionstring, string dbName);
        int updateMentorDetails(Mentor objMentor, MentorDetail objmentordetail, MentorQuestionary objmentorquestionary, string Connectionstring, string dbName);
        List<Mentor> GetMentorDetails(string Keyword, Int32 Id, int jtStartIndex, int jtPageSize, string jtSorting, out int RecordCount, string Connectionstring, string dbName, string roleid, string isfrom, string alphanumericsort);
        List<Venture> getVentureDetails(string Connectionstring, string dbName);
        List<MentorType> getMentorTypeDetails(string Connectionstring, string dbName);
        bool IsMentorExists(string MentorName, string Connectionstring, string dbName);
        List<Venture> getVenturePrefix(string Prefix, string Connectionstring, string dbName);
        bool DeleteMentorByMentorId(Int64 VentureId, string Connectionstring, string dbName);

        int DeleteMentorByMentorIdNew(Mentor mentor, string Connectionstring, string dbName);

        List<Mentor> GetAllLeadMentorsList(string Connectionstring, string dbName);
        Int64 getMentorIdbyMentorName(string Mentorname, string Connectionstring, string dbName);
        MentorDetail GetPendingMentorDetailsByID(long id, string Connectionstring, string dbName);
        MentorQuestionary GetMentorQuestionaryByMID(long id, string Connectionstring, string dbName);
        int updatePendingMentorDetails(Mentor objmentor,MentorDetail objmentordetail, MentorQuestionary objmentorquestionary, string Connectionstring, string dbName);
    }
}
