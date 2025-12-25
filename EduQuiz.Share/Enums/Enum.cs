using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduQuiz.Share.Enums
{
    public class Enum
    {
        public enum ClassStatus
        {
            [PgName("DRAFT")]
            DRAFT,

            [PgName("PUBLISHED")]
            PUBLISHED,

            [PgName("UNPUBLISHED")]
            UNPUBLISHED,

            [PgName("ONGOING")]
            ONGOING,

            [PgName("REMOVED")]
            REMOVED
        }

        public enum GroupCodeEnum 
        {
            ACADEMIC_REPORT
        }
    }
}
