using System;
using System.Collections.Generic;
using System.Text;

namespace BreakBrainGame
{
    public interface IGame
    {
        void ClearLvL();
        void CreateLVLlabel();
        void LVL1_Load();
        void LVL2_Load();
        void LVL3_Load();
    }
}
