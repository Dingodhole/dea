﻿using System;

public static class Config
{
    public static readonly string TOKEN = "MjgwMjAxNzUwMjQ0MzYwMTk0.C5keHw.Nj6fcpH5igw15CmcEcelaz1V5d4";

    public static readonly int MIN_CHAR_LENGTH = 7, LEADERBOARD_CAP = 20, LINE_COOLDOWN = 25000;

    public static readonly float RANK1 = 500, RANK2 = 2500, RANK3 = 5000, RANK4 = 10000, LINE_COST = 250, POUND_COST = 1000,

    KILO_COST = 2500, POUND_MULTIPLIER = 2, KILO_MULTIPLIER = 4, RESET_REWARD = 10000, HIGHEST_WHORE = 100, 
        
    HIGHEST_JUMP = 250, HIGHEST_STEAL = 500, MAX_RESOURCES = 1000, MIN_RESOURCES, TEMP_MULTIPLIER_RATE = 0.1f, MAX_TEMP_MULTIPLIER = 10;

    public static readonly string[] BANKS = { "Bank of America", "Wells Fargo Bank", "JPMorgan Chase Bank", "Capital One Bank",

    "RBC Bank", "USAA Bank", "Union Bank", "Morgan Stanley Bank" }, STORES = { "7-Eleven", "Speedway", "Couche-Tard", "QuikTrip",

    "Kroger", "Circle K" };

}