using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entity;
using DAL.DTO;
using DAL.Generic;

namespace DAL.Persistence
{
    public class RoleDAL : GenericDal<Role, short>
    {
        #region Construtor

        public RoleDAL()
        {

        }

        #endregion
        
        #region Metodos

        public static RoleDAL GeneratesRoleDAL()
        {
            return new RoleDAL();
        }
        
        #endregion
    }
}
