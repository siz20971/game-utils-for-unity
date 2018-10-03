namespace GamePadHelper
{
    public enum KeyPhase
    {
        NONE,
        DOWN,
        KEEP,
        UP
    }

    public enum GamePadKey
    {
        NONE,

        D_UP,
        D_DOWN,
        D_LEFT,
        D_RIGHT,

        L_STICK,
        R_STICK,

        LB,
        RB,
        LT,
        RT,

        BACK,
        START,

        X,
        Y,
        A,
        B,

        // Virtual

        // Virtual (Game Controller right button set.)
        //          FUNC_UP
        //  FUNC_LEFT       FUNC_RIGHT
        //          FUNC_DOWN
        FUNC_UP,
        FUNC_DOWN,
        FUNC_LEFT,
        FUNC_RIGHT,

        ANALOG_L_UP,
        ANALOG_L_DOWN,
        ANALOG_L_LEFT,
        ANALOG_L_RIGHT,

        ANALOG_R_UP,
        ANALOG_R_DOWN,
        ANALOG_R_LEFT,
        ANALOG_R_RIGHT
    }
}