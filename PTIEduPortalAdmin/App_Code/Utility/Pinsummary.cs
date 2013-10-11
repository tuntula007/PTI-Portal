
using System;
using System.Collections;

public partial class Pinsummary
{
    private static string Srn;
    private static string Centre;
    private static string Programme;
    private static int Usedpins;
    private static int Availpins;
    private static int Dispatchpin;
    private static double TotalDispatchedpin;
    private static double TotalUsedpin;
    private static double TotalAvailpin;
    private static double GroundTotalAllawi;


    public Pinsummary Clone()
    {
        // Create New Object
        Pinsummary deptl = new Pinsummary();

        deptl.srn = this.srn;
        deptl.centre = this.centre;
        deptl.programme = this.programme;
        deptl.usedpins = this.usedpins;
        deptl.availpins = this.availpins;
        deptl.dispatchpin = this.dispatchpin;
        deptl.totalDispatchedpin = this.totalDispatchedpin;
        deptl.totalUsedpin = this.totalUsedpin;
        deptl.totalAvailpin = this.totalAvailpin;
        deptl.groundTotalAllawi = this.groundTotalAllawi;

        return deptl;

    }
    public string srn
    {
        get
        {
            return Srn;
        }
        set
        {
            Srn = value;
        }
    }

    public string centre
    {
        get
        {
            return Centre;
        }
        set
        {
            Centre = value;
        }
    }


    public string programme
    {
        get
        {
            return Programme;
        }
        set
        {
            Programme = value;
        }
    }
    public int usedpins
    {
        get
        {
            return Usedpins;
        }
        set
        {
            Usedpins = value;
        }
    }

    public int availpins
    {
        get
        {
            return Availpins;
        }
        set
        {
            Availpins = value;
        }
    }

    public int dispatchpin
    {
        get
        {
            return Dispatchpin;
        }
        set
        {
            Dispatchpin = value;
        }
    }

    public double totalDispatchedpin
    {
        get
        {
            return TotalDispatchedpin;
        }
        set
        {
            TotalDispatchedpin = value;
        }
    }

    public double totalUsedpin
    {
        get
        {
            return TotalUsedpin;
        }
        set
        {
            TotalUsedpin = value;
        }
    }
    public double totalAvailpin
    {
        get
        {
            return TotalAvailpin;
        }
        set
        {
            TotalAvailpin = value;
        }
    }

    public double groundTotalAllawi
    {
        get
        {
            return GroundTotalAllawi;
        }
        set
        {
            GroundTotalAllawi = value;
        }
    }


}

