//[JsonIgnore]
//public double EnergyGain => (double)(10 + (Level * 0.3) - 0.3);

//[JsonIgnore]
//public double Noclegownia => 7.2 * EnergyGain;

//[JsonIgnore]
//public double Studnia => Level;

//[JsonIgnore]
//public double Akademia => Math.Round(5 + Math.Ceiling(Convert.ToDouble(Level / 10)) + 0.3, 2);

//[JsonIgnore]
//public double LevelFactor
//{
//    get
//    {
//        if (Level <= 49) return 1;
//        else if (Level <= 99) return 2;
//        else if (Level <= 149) return 4;
//        else if (Level <= 199) return 5;
//        else if (Level <= 249) return 8;
//        else if (Level <= 299) return 9;
//        else if (Level <= 349) return 11;
//        return 1;
//    }
//}

//// COMBAT ROUND //

//[JsonIgnore]
//public double AverageSkill { get; set; }

//[JsonIgnore]
//public double AverageEvasion { get; set; }

//[JsonIgnore]
//public double Win { get; set; }

//[JsonIgnore]
//public double Lost { get; set; }

//[JsonIgnore]
//public string Mob { get; set; }

//[JsonIgnore]
//public double AveragePD { get; set; }

//[JsonIgnore]
//public double DailyEnergy

//{
//    get
//    {
//        double enkaNaReset = 10 + (Level * 0.3 - 0.3);
//        return enkaNaReset * 24 * 0.3 + Level + (enkaNaReset * 24);
//    }
//}

//[JsonIgnore]
//public double ExpGranica => Level * Level * 50 * LevelFactor;

//[JsonIgnore]
//public double EnergiaLevel => Math.Max(0, Math.Round(ExpGranica / AveragePD + Lost));

//[JsonIgnore]
//public double SumaUBuniki => Math.Round(AverageSkill + AverageEvasion, 4);

//[JsonIgnore]
//public double UBdziennie => AverageSkill * DailyEnergy;

//[JsonIgnore]
//public double UnikiDziennie => AverageEvasion * DailyEnergy;

//[JsonIgnore]
//public double UBlevel => Math.Round(AverageSkill * EnergiaLevel);

//[JsonIgnore]
//public double UnikiLevel => Math.Round(AverageEvasion * EnergiaLevel);

//[JsonIgnore]
//public double SumaUBunikiLevel => Math.Round(SumaUBuniki * EnergiaLevel);

//[JsonIgnore]
//public double Koszt => Math.Round(EnergiaLevel / SumaUBunikiLevel, 4);