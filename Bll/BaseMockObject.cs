using System;

namespace NewPayDataTransformer.Model
{
    public class BaseMockObject
    {
        internal string LPad(string startText, int targetLength)
        {
            return LPad(startText,targetLength,"0");
        }

        internal string LPad(string startText, int targetLength, string padWithCharacter)
        {
            string retval = startText;
            int currentLength = retval.Length;
            while(currentLength < targetLength)
            {
                retval = padWithCharacter + retval;
                currentLength = retval.Length;
            }
            return retval;
        }

    }//end class
}//end namespace