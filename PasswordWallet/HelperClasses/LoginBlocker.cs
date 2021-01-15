using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordWallet.Logic.HelperClasses
{
    public class LoginBlocker
    {
        public bool isIPBlocked(int idIP)
        {
            return false;
        }

        public int userVerificationTime()
        {
            return 0;
        }
        private int getTimeForAccountBlocking()
        {
            return 0;
        }

        private int getTimeForIPBlocking()
        {
            return 0;
        }

        public string setMessageText(int incorrectTrials, int verificationTime)
        {
            return "You provided incorrect login data " + incorrectTrials + " times. \n Login verification is now extended to " + verificationTime + "s.";
        }
    }
}
