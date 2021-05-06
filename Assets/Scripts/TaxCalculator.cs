using UnityEngine;
using UnityEngine.UI;
using SpeechLib;

public class TaxCalculator : MonoBehaviour
{
    // Constant rate for the Medicare Levy
    const double MEDICARE_LEVY = 0.02;

    // Variables
    bool textToSpeechEnabled = true;
    public InputField grossSalaryInputField;
    public Dropdown timePeriod;
    public Text output;
    private void Start()
    {
        Speak("Welcome to the A.T.O. Tax Calculator");
    }

    // Run this function on the click event of your 'Calculate' button
    public void Calculate()
    {
        // Initialisation of variables
        double medicareLevyPaid = 0;
        double incomeTaxPaid = 0;

        // Input
        double grossSalaryInput = GetGrossSalary();
        string salaryPayPeriod = GetSalaryPayPeriod();

        // Calculations
        double grossYearlySalary = CalculateGrossYearlySalary(grossSalaryInput, salaryPayPeriod);
        double netIncome = CalculateNetIncome(grossYearlySalary, ref medicareLevyPaid, ref incomeTaxPaid);

        // Output
        OutputResults(medicareLevyPaid, incomeTaxPaid, netIncome);

        
    }

    private double GetGrossSalary()
    {
        // Get from user. E.g. input box
        // Validate the input (ensure it is a positive, valid number)
        return double.Parse(grossSalaryInputField.text);
    }

    private string GetSalaryPayPeriod()
    {
        // Get from user. E.g. combobox or radio buttons
        if (timePeriod.value == 0)
        {
            return "Weekly";
        }
        else if (timePeriod.value == 1)
        {
            return "Fortnightly";
        }
        else if (timePeriod.value == 2)
        {
            return "Monthly";
        }
        else
        {
            return "Yearly";
        }
        
    }

    private double CalculateGrossYearlySalary(double grossSalaryInput, string salaryPayPeriod)
    {
        // This is a stub, replace with the real calculation and return the result
        if (salaryPayPeriod == "Weekly")
        {
            return grossSalaryInput * 52;
        }
        else if (salaryPayPeriod == "Fortnightly")
        {
            return grossSalaryInput * 26;
        }
        else if (salaryPayPeriod == "Monthly")
        {
            return grossSalaryInput * 12;
        }
        else
        {
            return grossSalaryInput;
        }
    }

    private double CalculateNetIncome(double grossYearlySalary, ref double medicareLevyPaid, ref double incomeTaxPaid)
    {
        // This is a stub, replace with the real calculation and return the result
        medicareLevyPaid = CalculateMedicareLevy(grossYearlySalary);
        incomeTaxPaid = CalculateIncomeTax(grossYearlySalary);

        return grossYearlySalary - (medicareLevyPaid + incomeTaxPaid);
    }

    private double CalculateMedicareLevy(double grossYearlySalary)
    {
        // This is a stub, replace with the real calculation and return the result      
        return MEDICARE_LEVY * grossYearlySalary;
    }

    private double CalculateIncomeTax(double grossYearlySalary)
    {
        // This is a stub, replace with the real calculation and return the result
        if(grossYearlySalary <= 18000)
        {
            return 0;
        }
        else if(grossYearlySalary <= 37000)
        {
            return 0.19 * (grossYearlySalary - 18200);
        }
        else if(grossYearlySalary <= 87000)
        {
            return 0.325 * (grossYearlySalary - 37000) + 3572;
        }
        else if(grossYearlySalary <= 180000)
        {
            return 0.37 * (grossYearlySalary - 87000) + 19822;
        }
        else
        {
            return 0.45 * (grossYearlySalary - 180000) + 54232;
        }
        
    }

    private void OutputResults(double medicareLevyPaid, double incomeTaxPaid, double netIncome)
    {
        // Output the following to the GUI
        // "Medicare levy paid: $" + medicareLevyPaid.ToString("F2");
        // "Income tax paid: $" + incomeTaxPaid.ToString("F2");
        // "Net income: $" + netIncome.ToString("F2");
        output.text = $"Medicare levy paid: ${medicareLevyPaid}\nIncome tax paid: ${incomeTaxPaid}\nNet income: ${netIncome}";
    }

    // Text to Speech
    private void Speak(string textToSpeak)
    {
        if(textToSpeechEnabled)
        {
            SpVoice voice = new SpVoice();
            voice.Speak(textToSpeak);
        }
    }
}
