/*
 * Created by SharpDevelop.
 * User: CodeExplorer
 * Date: 23.12.2019
 * Time: 14:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

// you need this once (only), and it must be in this namespace
namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class
         | AttributeTargets.Method)]
    public sealed class ExtensionAttribute : Attribute {}
}

namespace Modulo2Simplificator
{
	
public static class StringExtension
{    
		
    /// <summary> Returns the number of occurences of a string within a string, optional comparison allows case and culture control. </summary>
    public static int Occurrences(this System.String input, string value)
    {
        if (String.IsNullOrEmpty(input)) return 0;
        if (String.IsNullOrEmpty(value)) return 0;

        int count    = 0;
        int position = 0;

        while ((position = input.IndexOf(value, position, StringComparison.Ordinal)) != -1)
        {
            position += value.Length;
            count++;
        }

        return count;
    }

    /// <summary> Returns the number of occurences of a single character within a string. </summary>
    public static int Occurrences(this System.String input, char value)
    {
        int count = 0;
        foreach (char c in input)
        	if (c == value) count++;
        return count;
    }
}
	
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
    public static bool IsNumeric(string s)
    {
        foreach (char c in s)
        {
            if (!char.IsDigit(c))
                return false;
        }

        return true;
    }

    public static bool IsLetter(string s)
    {
        foreach (char c in s)
        {
            if (!char.IsLetter(c))
                return false;
        }

        return true;
    }
    
		
		string ExpandEquation(string toExpand)
		{
		if (toExpand=="") return "";
		
		int index_start_value = toExpand.IndexOf('(');
		if (index_start_value<0) return "";
		
		string startValue = toExpand.Substring(0, index_start_value);
		string restOfValues = toExpand.Substring(index_start_value+1);
		
		if (restOfValues.EndsWith(")"))  // remove last  ) char
		restOfValues = restOfValues.Substring(0, restOfValues.Length - 1);
		
		string TheOperation = "";
		if (restOfValues.Contains("+")||restOfValues.Contains("-"))
		TheOperation = "+";  // + equal - in modulo 2 calcule
		else if (IsNumeric(restOfValues))
		TheOperation = "";
		else
		return "";
		
		if (startValue=="") return "";  // if we don't have start value return "";
		
		string[] entries = restOfValues.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
		string constructed = "";
		for (int i=0;i<entries.Length;i++)
		{
		constructed += startValue;
		if (IsNumeric(entries[i]))  // for numeric special case, in rest * is implicit operation
		constructed += "*";
		
		constructed += entries[i];
		
		if (i!=(entries.Length-1))  // if isn't last entry
		constructed += TheOperation;
		}
		
		return constructed;
		
		}
		
		void BubleSort(string[] ToSort)
		{
		
		int len = ToSort.Length;
		string temp;
		
		for (int i = 0; i < len; i++)
		{
			for (int j = 0; j < len-1; j++)
			  {
				if (ToSort[j].CompareTo(ToSort[j + 1]) > 0)
				  {
                  		 temp = ToSort[j];
                  		 ToSort[j] = ToSort[j + 1];
                   		 ToSort[j + 1] = temp;
				  }
            	 	 }
        	}
		
		}
		
		string[] SplitVariabiles(string ToSplit)
		{
		if (ToSplit=="") return new string[0];
		
		string[] splited = null;
		
		int Len = ToSplit.Length;
		
		int VariabileCount = 0;
		int CurrentVarCount = 0;
		int pos = 0;
		int old_pos = 0;
		int RunTimes = 0;
	
		while (RunTimes<2)
		{
		pos = 0;  // reinit position
		old_pos = 0;
		
		while (ToSplit!=""&&pos<ToSplit.Length)
		{
				
		// a variabile start with a letter and end with a number
		while(pos<ToSplit.Length&&char.IsLetter(ToSplit[pos]))  // until is a letter increment pos
		pos++;

		while(pos<ToSplit.Length&&char.IsDigit(ToSplit[pos]))  // until is a number increment pos
		pos++;
				
		if (RunTimes==0)
		{
		VariabileCount++;
		}
		else
		{
		splited[CurrentVarCount] = ToSplit.Substring(old_pos, pos-old_pos);
		CurrentVarCount++;

		}
		
		if (pos<ToSplit.Length&&ToSplit[pos]=='*')  // if is multiply skipp that char
		pos++;
		
		if (RunTimes==1)
		old_pos = pos;

		
		}
		
		if (RunTimes==0)
		splited = new string[VariabileCount];

		RunTimes++;
		}

				
		return splited;
		}
		
		string JoinStrings(string[] tojoin, string delimiter)
		{
			if (tojoin==null|| tojoin.Length==0)
			return "";
			
			string Joined = "";
			bool PreviousWasNumber = false;
			
			for (int k=0; k<tojoin.Length; k++) // Traversing string  
     			{
				if (tojoin[k]!="")
				{
				Joined += tojoin[k];
				
				if (k!=(tojoin.Length-1))  // if not last one add delimiter
				{
				if (delimiter!=null&&delimiter!="")
				Joined += delimiter;
				 else if (PreviousWasNumber&&IsNumeric(tojoin[k]))
				Joined += "*";
				 
				PreviousWasNumber = IsNumeric(tojoin[k]);
				}
				
				
				
				}
				
			}
			
		if (delimiter!=null&&delimiter!=""&&Joined.EndsWith(delimiter))
		Joined = Joined.Substring(0,Joined.Length - 1);
		
		return Joined;
		}
		
		string RemoveEquationDuplicates(string equation)
		{
		
		string[] operands = equation.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
		BubleSort(operands);  // we need to sort that string else won't remove any thing
					
    			for (int k=0; k<operands.Length; k++) // Traversing string  
     			{
    				int iter = k;
       			int counter = 0;
        			while (iter<operands.Length&&operands[iter]!=""&&operands[iter]==operands[k]) 
       			 {
				iter++;
				counter++;
     			        }
        			
        			if (counter>1)
        			{
        			if ((counter&1)==0)  // if the rest is 0
        			{
        			for (int l=k;l<(k+counter);l++)
				operands[l] = "";
        			}
        			else  // if the rest is 1
        			{
        			for (int l=k+1;l<(k+counter);l++)
				operands[l] = "";
        			}
        			
        			}
        			
   			}
    			
    			string joined = JoinStrings(operands, "+");  // here + is the separator
    			return joined;
		}
		
		string ExpandParenthesis(string equation)
		{
			// time to expand var1*(var2+var3) = var1*var2+var1*var3
			int PreviousFound = 0;
			int EndIndex = 0;
			string ToExpand = "";
			string ToReplace = "";
			
			while (PreviousFound<equation.Length&&(PreviousFound = equation.IndexOf('(', PreviousFound))>=0)
			{
			
			EndIndex = equation.IndexOf(')', PreviousFound);
			if (EndIndex<0) continue; // equation is malformed
			
			int RealStart = PreviousFound;
			while(RealStart>0&&equation[RealStart]!='+'&&equation[RealStart]!='*'&&equation[RealStart]!='-'&&equation[RealStart]!='/')
			RealStart--;  // until no more string or an operations starts 

			if (equation[RealStart]=='+'||equation[RealStart]=='*'||equation[RealStart]=='-'||equation[RealStart]=='/')
			RealStart++;  // we have to fix it
			
			ToExpand =  equation.Substring(RealStart, EndIndex-RealStart+1);
			ToReplace = ToExpand;
			int ComesAfter = 0;
			if ((EndIndex+1)<equation.Length&&equation[EndIndex+1]!='+'&&equation[EndIndex+1]!='-')
			{  // if comes something after ():
			ComesAfter = EndIndex+1;
						
			if (ComesAfter<equation.Length&&equation[ComesAfter]=='(')
			{
			ComesAfter = equation.IndexOf(')', ComesAfter)+1;
			}
			else
			{
			while(ComesAfter<equation.Length&&equation[ComesAfter]!='+'&&
			      equation[ComesAfter]!='*'&&equation[ComesAfter]!='-'&&equation[ComesAfter]!='/')
			ComesAfter++;
			}
			
			if (ComesAfter >EndIndex+1)  // if we have something
			{
				string ToBeAdded = equation.Substring(EndIndex+1, ComesAfter-(EndIndex+1));
				ToReplace = ToExpand+ToBeAdded;
				
				// time to remove useless chars:
				if (ToExpand.IndexOf('+')<0&&ToExpand.IndexOf('-')<0)
				ToExpand = ToExpand.Replace("(","").Replace(")","");  // remove uselless ()
				
				if (ToBeAdded.IndexOf('+')<0&&ToBeAdded.IndexOf('-')<0)
				ToBeAdded =  ToBeAdded.Replace("(","").Replace(")","");  // remove uselless ()
                               
				if (ToExpand.Contains("+")||ToExpand.Contains("-"))
				ToExpand = ToBeAdded+ToExpand;  // simplest form first
				else
				ToExpand =ToExpand+ToBeAdded;
				

				
			}
			
			}
			
			string expanded = ExpandEquation(ToExpand);
			
			if (expanded!="")
			{
			equation = equation.Replace(ToReplace, expanded);
			PreviousFound = RealStart+expanded.Length-1;
			//char After = equations[i][PreviousFound];
			
			}
			
			PreviousFound++;
			}
			PreviousFound = 0;
			equation = equation.Replace("(", "").Replace(")", "");
			
			return equation;
			
		}
		
		string SimplificatePerEquation(string equation)
		{
				
			int i,j;
			// time to expand var1*(var2+var3) = var1*var2+var1*var3
			equation = ExpandParenthesis(equation);

			// time to sort x variabiles after their number x3x2 => x2x3
			
			string[] operands = equation.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
			for (j=0;j<operands.Length;j++)
			{
			string[] ToSort = SplitVariabiles(operands[j]);
			if (ToSort!=null&&ToSort.Length>0)
			{
			BubleSort(ToSort);
			//string[] ToSort = new string[]{"x2", "x1", "x3", "4"};
			
// simplificate multiplied numbers:
// 2*x1*x2 = ""  and 1*x1*x2 = x1x2
// 2*2*3 = 12 mod 2 = 0
			int multiplied = 1;
			int CurrentPos = 0;

			while (CurrentPos<ToSort.Length&&IsNumeric(ToSort[CurrentPos]))
			{
			multiplied = multiplied&(Int32.Parse(ToSort[CurrentPos])&1);
			CurrentPos++;
			}
			
			if (CurrentPos>0)  // if we have at last one number
			{
			if ((multiplied&1)==0)  // if the rest is 0
			{
			for (int k=0;k<ToSort.Length;k++)
			ToSort[k] = "";
			}
			else  // if the rest is 1
			{
			if (ToSort.Length==1&&IsNumeric(ToSort[0]))
			{  // one exception here, we still have to add 1
			ToSort[0] = "1";
			}
			else
			{
			for (int k=0;k<CurrentPos;k++)
			ToSort[k] = "";
			}
			}
			
			}
			// end of number simplifications
			
			// now check operands for duplicates and remove them
    			for (int k=0; k<ToSort.Length; k++) // Traversing string  
     			{
    				int iter = k;
       			int counter = 0;
        			while (iter<ToSort.Length&&ToSort[iter]!=""&&ToSort[iter]==ToSort[k]) 
       			 {
					if (counter>0)  // keep the first occurence
					ToSort[iter] = "";  // remove that string
					
				iter++;
				counter++;
				
     			        }      
   			}
   			// end of duplicates operands removing
   			if (checkBox1.Checked)
   			operands[j] = JoinStrings(ToSort, "*");
   			else
			operands[j] = JoinStrings(ToSort, "");  // here is multiplication so we don't need delimiter
			// string ToReplace = operands[j];
			// equation = equation.Replace(ToReplace, JoinedBack);  // replace string
			
			
			}  // end of ToSort
			
			}
			
			// join operands instead of replace!
			equation =  JoinStrings(operands, "+");
			
			// time to check if equation contains duplicates which should be removed
			equation = RemoveEquationDuplicates(equation);
			return equation;
		}
		
		int CommonCount(string[] firstPart, string[] secondPart)
		{
		if (firstPart==null||secondPart==null) return 0;
		
		if (firstPart.Length==0||secondPart.Length==0) return 0;
		
		int count = 0;
		
		for (int i=0;i<firstPart.Length;i++)
			for (int j=0;j<secondPart.Length;j++)
				if (firstPart[i]==secondPart[j])
				count++;
		
		return count;
			
		
		}
				  
		void SimplificateSystem(string[] equations)
		{		
		string[] firstPart = null;
		string[] secondPart = null;
		
		int DecreaseCount = 0;
		int ToSimpWithIndex = -1;
		int common = 0;
		int NewLen = 0;
		int Reduction = 0;
		
		bool WasSimplificated = false;
		do
		{
		WasSimplificated = false;
		for (int i=0;i<equations.Length;i++)
		{
			firstPart = equations[i].Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
			
			DecreaseCount = 0;
			ToSimpWithIndex = -1;
			
			for (int j=0;j<equations.Length;j++)
			{
				if (i==j) continue;
				
				secondPart = equations[j].Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
				
				common = CommonCount(firstPart, secondPart);
				if (common<=0) continue;  // if equation have nothing in common
				
				NewLen = firstPart.Length+secondPart.Length-(common<<1);
				
				if (NewLen<firstPart.Length)  // only if we actually simplificated something
				{
					Reduction = NewLen-firstPart.Length;  // smalller is better
					if ((ToSimpWithIndex==-1)||Reduction<DecreaseCount)
					{  // if first time or current value smaller then previous
						
				int HowManyInitial = StringExtension.Occurrences(GetUsedVariabiles(equations[i]), ',');
				string Joined_equation = equations[i]+"+"+equations[j];  // we add to curent equation that
				Joined_equation= RemoveEquationDuplicates(Joined_equation);
				
				int HowManyAfter = StringExtension.Occurrences(GetUsedVariabiles(Joined_equation), ',');
				if (HowManyAfter<=HowManyInitial)
				{
					DecreaseCount = Reduction;
					ToSimpWithIndex = j;
				}
				
					}
				
				}
				
				if (NewLen==firstPart.Length)  // we could have something else
				{
				int BilinearBeforeCount = BilinearCount(equations[i]);
				int VariablesInitial = StringExtension.Occurrences(GetUsedVariabiles(equations[i]), ',');
				string Joined_equation2 = equations[i]+"+"+equations[j];  // we add to curent equation that
				Joined_equation2 = RemoveEquationDuplicates(Joined_equation2);
				
				int BilinearAfterCount = BilinearCount(Joined_equation2);
				int VariablesAfter = StringExtension.Occurrences(GetUsedVariabiles(Joined_equation2), ',');
				
				if (BilinearAfterCount<BilinearBeforeCount&&VariablesAfter<=VariablesInitial)
				{
				equations[i] = Joined_equation2;
				WasSimplificated = true;
				
				}
				
				if (BilinearAfterCount<=BilinearBeforeCount&&VariablesAfter<VariablesInitial)
				{
				equations[i] = Joined_equation2;
				WasSimplificated = true;
				}
				

				}
					
				
			}
			
			if (ToSimpWithIndex!=-1)
			{
			
			int HowManyInitial = StringExtension.Occurrences(GetUsedVariabiles(equations[i]), ',');
			string equationBak = equations[i] ;
			
				equations[i] += "+"+equations[ToSimpWithIndex];  // we add to curent equation that
				equations[i] = RemoveEquationDuplicates(equations[i]);
				
				int HowManyAfter = StringExtension.Occurrences(GetUsedVariabiles(equations[i]), ',');
				if (HowManyAfter<=HowManyInitial)
				WasSimplificated = true;
				else
				equations[i] = equationBak;
			}
							
		}
		}
		while (WasSimplificated);
		
		}
		
		// main of program:
		void SimplificateEquations(string[] equations)
		{

			for (int i=0;i<equations.Length;i++)
			equations[i] = SimplificatePerEquation(equations[i]);
			
			

		}
		
		void Button2Click(object sender, EventArgs e)
		{
			string orig_equations = "";
			if (textBox1.Text!=""&&File.Exists(textBox1.Text))
			orig_equations = File.ReadAllText(textBox1.Text);
			else
			orig_equations = textBox2.Text;
			
			orig_equations = orig_equations.Replace(" ", "").Replace("*", "");  // we remove space, *
			orig_equations = orig_equations.Replace("mod2","").Replace("mod(", "(").Replace(",2)", ")"); // replace modulo craps
			
			if (orig_equations=="")
			{
			label3.Text = "Empty equations, wtf???";
			return;
			}
			
			orig_equations = orig_equations.Replace("=0", "");  // this is default: equation = 0;
			orig_equations = orig_equations.Replace("=1", "+1");  // if is equation = 1;
			
			if (orig_equations=="")
			{
			label3.Text = "Empty equations, wtf???";
			return;
			}
			
			string[] equations = null;
			if (orig_equations.Contains(":"))
			{
			orig_equations = orig_equations.Replace( Environment.NewLine, "");
			equations = orig_equations.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
			}
			else			
			equations = orig_equations.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			
			SimplificateEquations(equations);
			SimplificateSystem(equations);
			
			
			if (textBox1.Text!=""&&File.Exists(textBox1.Text))
			{
			string OutFile = Path.GetFileNameWithoutExtension(textBox1.Text)+"_simplified"+Path.GetExtension(textBox1.Text);
			
			using(StreamWriter writetext = new StreamWriter(OutFile))
			{
			for (int i=0;i<equations.Length;i++)
			{
			writetext.WriteLine(equations[i]);
			writetext.WriteLine();
			}
			}
			
			label3.Text = "Simplifications saved to file. Done.";
			}
			else
			{
			textBox3.Text  = "";  // remove existing text
			for (int i=0;i<equations.Length;i++)
			{
			textBox3.Text  += equations[i]+Environment.NewLine;
			}
			}
			
		}
		
		
		
		void Button1Click(object sender, EventArgs e)
		{
   		 var FD = new System.Windows.Forms.OpenFileDialog();
    		DialogResult result = FD.ShowDialog(); // Show the dialog.
    		if (result == DialogResult.OK) // Test result.
    		textBox1.Text= FD.FileName;

		}
		
		string GetFreeVariabiles(string equation)
		{
			
			equation = ExpandParenthesis(equation);
			
			string PossibleFreeVars = "";
			string[] operands = equation.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i=0;i<operands.Length;i++)
			{
			string[] ToSort = SplitVariabiles(operands[i]);
			if (ToSort!=null&&ToSort.Length==1)
			if (!PossibleFreeVars.Contains(ToSort[0]))
			PossibleFreeVars += ToSort[0]+"+";
			}
			
			string RealFreeVars = "";
			int index = 0;
			string[] PosibleSplited = PossibleFreeVars.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
			for (int j=0;j<PosibleSplited.Length;j++)
			{
				index = 0;
				bool IsFreeVariabile = true;
				while ((index = equation.IndexOf(PosibleSplited[j], index))>=0)
				{
				IsFreeVariabile = true;
				
				if ((index-1)>=0)
				{
				char Before = equation[index-1];
				if (Before!='+'&&Before!='-')
				{
				IsFreeVariabile = false;
				//break;
				}
				}
				
				if ((index+PosibleSplited[j].Length)<equation.Length)
				{
				char After = equation[index+PosibleSplited[j].Length];
				if (After!='+'&&After!='-')
				{
				IsFreeVariabile = false;
				//break;
				}
				}
				
				index++;

				}
				
				if (IsFreeVariabile) 	RealFreeVars += PosibleSplited[j]+"+";
			}
			
		if (RealFreeVars.EndsWith("+")) RealFreeVars = RealFreeVars.Substring(0,RealFreeVars.Length-1);
			
		return RealFreeVars;
		}
		
		
		string GetUsedVariabiles(string equation)
		{
			
			equation = ExpandParenthesis(equation);
			
			string UsedVars = "";
			string[] operands = equation.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i=0;i<operands.Length;i++)
			{
			string[] ToSort = SplitVariabiles(operands[i]);
			if (ToSort==null||ToSort.Length<=0)
			continue;
			
			for (int j=0;j<ToSort.Length;j++)
			{
			
			if (!UsedVars.Contains(ToSort[j]))
			UsedVars += ToSort[j]+",";
			}
			
			}
			
			return UsedVars;
			
		}
		
		int BilinearCount(string equation)
		{
		if (equation==null||equation.Length==0) return 0;
		
		int BilinearCount = 0;
		
			string[] operands = equation.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i=0;i<operands.Length;i++)
			{
				if ((operands[i].Length<=0)||char.IsDigit(operands[i][0])) continue;  // is not bilinear
				
				int pos = 0;
				if (char.IsLetter(operands[i][0]))  // we have one variable
				{
				while (pos<operands[i].Length&&char.IsLetter(operands[i][pos]))  // until is letter
				pos++;
				
				while (pos<operands[i].Length&&char.IsDigit(operands[i][pos]))  // until is digit
				pos++;
				
				if (pos<operands[i].Length&&operands[i][pos]=='*')  // if is multiply skipp that char
				pos++;
				
				if (pos<operands[i].Length&&char.IsLetter(operands[i][pos]))  // if afterwards is letter
				{
				BilinearCount++;
				continue;
				}
				
				
				}
				
			
			}
		
		return BilinearCount;
		
		
		}
		
		
		string GetBilinear(string equation)
		{
		if (equation==null||equation.Length==0) return "";
		
		string BilinearPart = "";
		
			string[] operands = equation.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i=0;i<operands.Length;i++)
			{
				if ((operands[i].Length<=0)||char.IsDigit(operands[i][0])) continue;  // is not bilinear
				
				int pos = 0;
				if (char.IsLetter(operands[i][0]))  // we have one variable
				{
				while (pos<operands[i].Length&&char.IsLetter(operands[i][pos]))  // until is letter
				pos++;
				
				while (pos<operands[i].Length&&char.IsDigit(operands[i][pos]))  // until is digit
				pos++;
				
				if (pos<operands[i].Length&&operands[i][pos]=='*')  // if is multiply skipp that char
				pos++;
				
				if (pos<operands[i].Length&&char.IsLetter(operands[i][pos]))  // if afterwards is letter
				{
				BilinearPart += operands[i];
				BilinearPart +="+";
				continue;
				}
				
				
				}
				
			
			}
		
		if (BilinearPart.EndsWith("+")) BilinearPart = BilinearPart.Substring(0, BilinearPart.Length-1);
		return BilinearPart;
		
		
		}
	

		Dictionary<string, int> GetBilinearCommons(string bilinears)
			{
			Dictionary<string, int> dict = new Dictionary<string, int>();
			
			if (!bilinears.Contains("+")||(!bilinears.Contains("*")))  // means we have nothing to do
			return dict;
			
			string[] operands = bilinears.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
			string [][] splitedvars = new string[operands.Length][];
			for (int i=0;i<operands.Length;i++)
			splitedvars[i] = operands[i].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
			
			List<string> uniqueVariables = new List<string>();
			for (int i=0;i<operands.Length;i++)
			for (int k=0;k<splitedvars[i].Length;k++)
			{
				if (!uniqueVariables.Contains(splitedvars[i][k])) // if variable not already there
				uniqueVariables.Add(splitedvars[i][k]);  // add it
			}
			
			foreach (string variable in uniqueVariables)
			{
			
				for (int i=0;i<operands.Length;i++)
				for (int k=0;k<splitedvars[i].Length;k++)
				{
				
					if (splitedvars[i][k]==variable)  // if we have a match
					{
				   	if (!dict.ContainsKey(variable))  // it doesn't already contain the key
				    	{
				    		dict.Add(variable, 1);  // we found it 1 time
				    	}
				    	else
				   	{
				   	 dict[variable]++;  // increment found times
				    	}
				    
				}
				}
			
			
			}
			
			
			Dictionary<string, int> OnlyBiggerThen1 = new Dictionary<string, int>();
			// keep only the ones who have occurence>1
			foreach (KeyValuePair<string, int> pair in dict)
			{
    				if (pair.Value>1)
    				OnlyBiggerThen1.Add(pair.Key, pair.Value);
			}

// Sort Dictionary by Value:
OnlyBiggerThen1 = OnlyBiggerThen1.OrderByDescending(u => u.Value).ToDictionary(z => z.Key, y => y.Value);

			return OnlyBiggerThen1;
			}
	
		string JoinFromIndexes(string[] to_join, List<int> indexes)
		{
			string Produced = "";
			foreach ( int index in indexes)
			{
			Produced += to_join[index];
			Produced += "+";
			}
			
			if (indexes.Count<to_join.Length)
			{
				for (int i=0;i<to_join.Length;i++)
					if (!indexes.Contains(i))
					{
					Produced += to_join[i];
					Produced += "+";
					}
			}
			
		if (Produced.EndsWith("+")) Produced = Produced.Substring(0, Produced.Length-1);
		
		return Produced;
		}
		
		Dictionary<int, int> GetParenthesisIndexLen(string target)
		{
			Dictionary<int, int> Pars = new Dictionary<int, int>();
			if (target==null||target.Length<=0) return Pars;
			
			if (!target.Contains("(")&&target.Contains(")"))  // if not branches opened/closed
			return Pars;
			
			int OpenedBranches = 0;
			int index = 0;
			int FirstIndex = -1;
			
			while ((index = target.IndexOfAny(new char[]{'(',')'}, index)) != -1)
    			{
			if (target[index] == '(')
			{
			OpenedBranches++;
			if  (FirstIndex==-1)
			FirstIndex = index;
			}
			
			if (target[index] == ')')
			OpenedBranches--;
			
			if (OpenedBranches==0)
			{
			int Len = index-FirstIndex+1;
			string Kriminal = target.Substring(FirstIndex, Len);
			Pars.Add(FirstIndex, Len);
			FirstIndex = -1;
			}

			index++; // increment: move to next postion
    			}

			return Pars;
			
		
		}
		
		
		Dictionary<int, int> ParenthesisToFullIndexLen(string target, Dictionary<int, int> Pars)
		{
			Dictionary<int, int> NewPars = new Dictionary<int, int>();
			if (target==null||target.Length<=0) return NewPars;
			if (Pars.Count<=0)  return NewPars;
			
				foreach(KeyValuePair<int, int> kvp in Pars)
				{
			    	int StarIndex = kvp.Key;
			    	while(StarIndex>0&&target[StarIndex]!='+'&&target[StarIndex]!='-')
				StarIndex--;
			    	
			    	if (StarIndex!=0)
				StarIndex++;
			    	
			    	int EndIndex = kvp.Key+kvp.Value;
			    	while(EndIndex<target.Length&&target[EndIndex]!='+'&&target[EndIndex]!='-')
				EndIndex++;
			    	
			    	NewPars.Add(StarIndex, EndIndex-StarIndex);
			  	
			     	}
		
		return NewPars;
			
		}
		
		string RemoveFreeTerm(string target, string toRemove)
		{
		
			if (target==null||target=="") return "";
			string ToReturn = target;
			
			if (toRemove==null||toRemove=="") return ToReturn;
			
			int index = 0;
			while ((index = ToReturn.IndexOf(toRemove, index)) != -1)
    			{
				if (((index+toRemove.Length)==(ToReturn.Length-1))||((index+toRemove.Length)==(ToReturn.Length))||
				    ((index+toRemove.Length)<ToReturn.Length&&(ToReturn[index+toRemove.Length]=='-'||ToReturn[index+toRemove.Length]=='+')))
				{  // if the char after is + or -
				if (((index-1)<0)||((index-1)>=0&&(ToReturn[index-1]=='-'||ToReturn[index-1]=='+')))
				{  // if the char before is + or -
					
					string ToRemoveNew = toRemove;
					
					if (((index+toRemove.Length)<ToReturn.Length)&&(ToReturn[index+toRemove.Length]=='-'))
					ToRemoveNew += '-';
					 
					if (((index+toRemove.Length)<ToReturn.Length)&&(ToReturn[index+toRemove.Length]=='+'))
					ToRemoveNew += '+';
					
					ToReturn= ToReturn.Remove(index, ToRemoveNew.Length);
					continue;
				}
				}
			index++; // increment: move to next postion
			}
			
			if (ToReturn.StartsWith("+")||ToReturn.StartsWith("-"))
			ToReturn = ToReturn.Substring(1);  // remove first char
			if (ToReturn.EndsWith("+")||ToReturn.EndsWith("-"))
			ToReturn = ToReturn.Substring(0, ToReturn.Length-1);  // remove last char
			
			return ToReturn;
			
		}
		
		
		void Button3Click(object sender, EventArgs e)
		{  // second button
						
			string orig_equations = "";
			if (textBox1.Text!=""&&File.Exists(textBox1.Text))
			orig_equations = File.ReadAllText(textBox1.Text);
			else
			orig_equations = textBox2.Text;
			
			orig_equations = orig_equations.Replace(" ", "").Replace("*", "");  // we remove space, *
			orig_equations = orig_equations.Replace("mod2","").Replace("mod(", "(").Replace(",2)", ")"); // replace modulo craps
			
			if (orig_equations=="")
			{
			label3.Text = "Empty equations, wtf???";
			return;
			}
			
			orig_equations = orig_equations.Replace("=0", "");  // this is default: equation = 0;
			orig_equations = orig_equations.Replace("=1", "+1");  // if is equation = 1;
			
			if (orig_equations=="")
			{
			label3.Text = "Empty equations, wtf???";
			return;
			}
			
			string[] equations = null;
			if (orig_equations.Contains(":"))
			{
			orig_equations = orig_equations.Replace( Environment.NewLine, "");
			equations = orig_equations.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
			}
			else			
			equations = orig_equations.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			
			bool OLD_Resinsert = checkBox1.Checked;
			checkBox1.Checked = true;  // just to make sure * is inserted
			
			for (int i=0;i<equations.Length;i++)
			{
			equations[i] = SimplificatePerEquation(equations[i]); // sort equations before
			
			string BilinearPart = GetBilinear(equations[i]);

			if (BilinearPart!="")  // if we have any bilinear
			{
			int bilnear_count = BilinearCount(BilinearPart);
			Dictionary<string, int> Common =  GetBilinearCommons(BilinearPart);
			List<int> AlreadyVisitedPositions = new List<int>();
			
			if (Common.Count>0)  // if we have varible founded at last 2 times
			{
			
			string[] operands = equations[i].Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
			string [][] splitedvars = new string[operands.Length][];
			for (int j=0;j<operands.Length;j++)
			splitedvars[j] = operands[j].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
			
			foreach (KeyValuePair<string, int> pair in Common)
			{
    			
				for (int j=0; j<operands.Length;j++)
				{
				if (AlreadyVisitedPositions.Contains(j)) continue;  // if current index is already there
				
				for (int k=0;k<splitedvars[j].Length;k++)
				{
				
					if (splitedvars[j][k]==pair.Key)  // if we have a match
					AlreadyVisitedPositions.Add(j);
					
				}
				
				}
				
			}  // end of Key Pairs enum
			
			// This will sort equations:
			equations[i] = JoinFromIndexes(operands, AlreadyVisitedPositions);
			
			// we split equations[i]:
			operands = equations[i].Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
			splitedvars = new string[operands.Length][];
			for (int j=0;j<operands.Length;j++)
			splitedvars[j] = operands[j].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
			
			// first time factorize search
			foreach (string str_pair in Common.Keys)
			{
		
				int StartIndex = -1;
				int EndIndex = -1;
				
				for (int j=0; j<operands.Length;j++)
				{
				
				for (int k=0;k<splitedvars[j].Length;k++)
				{
					if (!splitedvars[j][k].Contains("(")&&!splitedvars[j][k].Contains(")")&&
					    splitedvars[j][k]==str_pair)  // if we have a match
					{
					if (operands[j] ==str_pair)  // if one left alone variable
					splitedvars[j][k] = "1";
					else
					splitedvars[j][k] = "";  // kill the sheet we are trying to factorize
					if (StartIndex==-1)  // if the first time
					{
					StartIndex = j;
					EndIndex = j;
					}
					else
					{
					EndIndex++;  // if not first time
					}
					
					}
					

					
				  }
				
				}  // for operands loop
				
				if (StartIndex!=-1)  // if founded something
				{
				
				string Constructed   = str_pair+"*(";
				 for (int newk=StartIndex;newk<=EndIndex;newk++)
				 {
				Constructed += JoinStrings(splitedvars[newk], "*");
				 if (newk!=EndIndex)
				 Constructed += "+";
				 }
				Constructed +=  ")";
				
				splitedvars[StartIndex][0] = Constructed;  // the first item is our factorized thing
				
				for (int lame=1;lame<splitedvars[StartIndex].Length;lame++)
				splitedvars[StartIndex][lame] = "";
				
				 for (int newk=StartIndex+1;newk<=EndIndex;newk++)
				 {
				for (int lame=0;lame<splitedvars[newk].Length;lame++)
				splitedvars[newk][lame] = "";
				 }
				
				}
								
				
				
			}  // foreach common pair  end
			
				for (int j=0;j<operands.Length;j++)
				operands[j] = JoinStrings(splitedvars[j], "*");

				equations[i] = JoinStrings(operands, "+");
			
			}  // end of if Common.Count>0
			
			}

			if (equations[i].Contains("(")&&equations[i].Contains(")"))  // if branches opened/closed
			{
			    Dictionary<string, string> replaces = new Dictionary<string, string>();
				do
				{
			   replaces = new Dictionary<string, string>();
			    Dictionary<int, int> paranteze = GetParenthesisIndexLen(equations[i]);
			    if (paranteze.Count>0)
			    {
			      
			    	foreach(KeyValuePair<int, int> kvp in paranteze)
				{
			    		string Old_String = equations[i].Substring(kvp.Key, kvp.Value);
			    		string ChangedABit = Old_String;
			    		if (ChangedABit.StartsWith("("))
			    		ChangedABit = ChangedABit.Substring(1);  // remove first char
			    		if (ChangedABit.EndsWith(")"))
			    		ChangedABit = ChangedABit.Substring(0, ChangedABit .Length-1);  // remove last char
			    		
			    		Dictionary<string, int> Common =  GetBilinearCommons(ChangedABit );
			    		int OperandsCount = StringExtension.Occurrences(ChangedABit, "+")+StringExtension.Occurrences(ChangedABit, "-")+1;
			    		if (Common.Count>0&&Common.First().Value==OperandsCount)
			    		{  // if all operands can factorize
			    		
			string[] operands2 = ChangedABit.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
			string [][] splitedvars2 = new string[operands2.Length][];
			for (int j=0;j<operands2.Length;j++)
			splitedvars2[j] = operands2[j].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
			
				int StartIndex = -1;
				int EndIndex = -1;
				
				for (int j=0; j<operands2.Length;j++)
				{
				
				for (int k=0;k<splitedvars2[j].Length;k++)
				{
					if (!splitedvars2[j][k].Contains("(")&&!splitedvars2[j][k].Contains(")")&&
					    splitedvars2[j][k]==Common.First().Key)  // if we have a match
					{
					if (operands2[j] ==Common.First().Key)  // if one left alone variable
					splitedvars2[j][k] = "1";
					else
					splitedvars2[j][k] = "";  // kill the sheet we are trying to factorize
					if (StartIndex==-1)  // if the first time
					{
					StartIndex = j;
					EndIndex = j;
					}
					else
					{
					EndIndex++;  // if not first time
					}
					
					}
					

					
				  }
				
				}  // for operands loop
				
				
				if (StartIndex!=-1)  // if founded something
				{
				
				string Constructed   = Common.First().Key+"*(";
				 for (int newk=StartIndex;newk<=EndIndex;newk++)
				 {
				Constructed += JoinStrings(splitedvars2[newk], "*");
				 if (newk!=EndIndex)
				 Constructed += "+";
				 }
				Constructed +=  ")";
				
				replaces.Add(Old_String, Constructed);
				
				
				
				} // end if StartIndex!=-1
				
		
			    		}  //   if all operands can factorize
				}
			    	

			    }
			    
			    
			    	foreach(KeyValuePair<string, string> kvp in replaces)
			    	equations[i] = equations[i].Replace(kvp.Key, kvp.Value);
			     	
				}  // do while we replaced something
				while (replaces.Count>0);
			    
			}  //   end if branches opened/closed
			
			
			if (equations[i].Contains("(")&&equations[i].Contains(")"))  // if branches opened/closed
			{
			 Dictionary<int, int> paranteze = GetParenthesisIndexLen(equations[i]);
			 Dictionary<int, int> FullIndex = ParenthesisToFullIndexLen(equations[i], paranteze);
			 string Free_terms = equations[i];
			 string NewEquation = equations[i];
			  	
			  	foreach(KeyValuePair<int, int> kvp in FullIndex)
				{
			    		string Old_String = equations[i].Substring(kvp.Key, kvp.Value);
			    		string ToReplace = Old_String;
			    		if ((kvp.Key+kvp.Value)<equations[i].Length&&equations[i][kvp.Key+kvp.Value]=='+')
			    		ToReplace += '+';
			    		if ((kvp.Key+kvp.Value)<equations[i].Length&&equations[i][kvp.Key+kvp.Value]=='-')
			    		ToReplace += '-';
			    		Free_terms = Free_terms.Replace(ToReplace, "");  // remove old  Parenthesis terms
			    		
			 	}
			  	
			  	List<string> AllParantheze = new List<string>();
			  	// first time keep a list with all Parantheze
			  	foreach(KeyValuePair<int, int> kvp in paranteze)
				{
			  	string Paranteza = equations[i].Substring(kvp.Key, kvp.Value);
			  	if (!AllParantheze.Contains(Paranteza))
			  		AllParantheze.Add(Paranteza);
			  	
			  	}
			  	
			  	foreach(string paranteza in AllParantheze)
				{

			  		string Factorized = "";
			  		
					int TargetIndex = 0;
			  		foreach(KeyValuePair<int, int> kvp in FullIndex)
					{
			  			
			  			KeyValuePair<int, int> PartialParantheze = paranteze.ElementAt(TargetIndex);
			  			
			  			if (equations[i].Substring(PartialParantheze.Key, PartialParantheze.Value)==paranteza)
			  			{
			  			
			  			string FullString = equations[i].Substring(kvp.Key, kvp.Value);
			  			NewEquation = NewEquation.Replace("+"+FullString, "");
			  			NewEquation = NewEquation.Replace(FullString, "");
			  			
			  			string PartialString = FullString.Replace("*"+paranteza, "");
			  			PartialString = PartialString.Replace(paranteza, "");
			  			Factorized += PartialString;
			  			Factorized += "+";
			  			
			  			}
			  									
						TargetIndex++;  // increment index please
			  		}
			  		
			  		if (Factorized!="")
			  		{
			    		if (Factorized.EndsWith("+"))
			    		Factorized = Factorized.Substring(0, Factorized .Length-1);  // remove last char
			    		Factorized = "("+Factorized+")"+"*"+paranteza;
			    		
			    		if (NewEquation!=""&&!NewEquation.StartsWith("+"))
			    		Factorized += "+";
			    		
			    		NewEquation = Factorized+NewEquation;
			    		
			  		}
			  			
			  		

			  	
			  	}  // foreach(string paranteza in AllParantheze)
			  	
			  	string[] FreeTermOperands = Free_terms.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
			  	int FreeTermContainsOne = 0;
			  	
			  	for (int k=0;k<FreeTermOperands.Length;k++)
				{
					if (FreeTermOperands[k]=="1")
					{
					FreeTermContainsOne = 1;
					break;
					}
				}
			  	
			  	foreach(string paranteza in AllParantheze)
				{
			  				  		
			  	string ParantezaNoua = paranteza;
			  		if (ParantezaNoua.StartsWith("("))
			    		ParantezaNoua = ParantezaNoua.Substring(1);  // remove first char
			    		if (ParantezaNoua.EndsWith(")"))
			    		ParantezaNoua = ParantezaNoua.Substring(0, ParantezaNoua.Length-1);  // remove last char
			    		
			  	string[] operands1 = ParantezaNoua.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
			  	int FoundCount = 0;
			  	int OpContainsOne = 0;
			  	
			  	
			  	for (int k=0;k<operands1.Length;k++)
				{
					if (operands1[k]=="1")
					{
					OpContainsOne = 1;
					break;
					}
				}
			  	
			  	for (int j=0; j<FreeTermOperands.Length;j++)
				{
				
				for (int k=0;k<operands1.Length;k++)
				{
					if (operands1[k]==FreeTermOperands[j])
					FoundCount ++;
				}
				
			  	}
			  	
			  	if (FoundCount ==operands1.Length||(FoundCount+OpContainsOne) ==operands1.Length)
			  	{  // if we found something
			  		
			  		string Factorized = "";
			  		int TargetIndex = 0;
			  		foreach(KeyValuePair<int, int> kvp in FullIndex)
					{
			  			
			  			KeyValuePair<int, int> PartialParantheze = paranteze.ElementAt(TargetIndex);
			  			
			  			if (equations[i].Substring(PartialParantheze.Key, PartialParantheze.Value)==paranteza)
			  			{
			  			string FullString = equations[i].Substring(kvp.Key, kvp.Value);
			  			NewEquation = NewEquation.Replace("+"+FullString, "");
			  			NewEquation = NewEquation.Replace(FullString, "");
			  			
			  			string PartialString = FullString.Replace("*"+paranteza, "");
			  			PartialString = PartialString.Replace(paranteza, "");
			  			
			  			string Rebuild_crap = "("+PartialString+")"+"*"+paranteza;
			  			NewEquation = NewEquation.Replace("+"+Rebuild_crap, "");
			  			NewEquation = NewEquation.Replace(Rebuild_crap, "");
			  			
			  			Factorized += PartialString;
			  			Factorized += "+";
			  			}
			  			
			  			TargetIndex++;  // increment index please
			  		}
			  		
			  		if (Factorized!="")
			  		{
			  		
			  	for (int k=0;k<operands1.Length;k++)
				{
					if (operands1[k]!="")
					NewEquation = RemoveFreeTerm(NewEquation, operands1[k]);	
				}
			  		
			    		if (Factorized.EndsWith("+"))
			    		Factorized = Factorized.Substring(0, Factorized .Length-1);  // remove last char
			    		Factorized = "("+Factorized+"+1"+")"+"*"+paranteza;
			    		
			    		if (OpContainsOne==1&&FreeTermContainsOne==0)
			    		Factorized += "+1";  // we need to fix this crap
			    		
			    		if (NewEquation!=""&&!NewEquation.StartsWith("+"))
			    		Factorized += "+";
			    		
			    		NewEquation = Factorized+NewEquation;
			    		
			  		}
			  		

			  	}
			  	
			  	
			  	
			  	}
			  	
			  	equations[i] = NewEquation;
			  	
			  	
			 
			}
			
			string FreeVarible = GetFreeVariabiles(equations[i]);
			if (!FreeVarible.Contains("+")&&!FreeVarible.Contains("-"))  // if only one variable
			{
			Dictionary<string, int> Common =  GetBilinearCommons(equations[i]);			
			if (Common.Count>0)  // if we have varible founded at last 2 times
			{
			int OperandsCount = StringExtension.Occurrences(equations[i], "+")+StringExtension.Occurrences(equations[i], "-")+1;
			if (OperandsCount>=Common.First().Value)
			{
			
			// we split equations[i]:
			string[] operands = equations[i].Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
			string [][] splitedvars = new string[operands.Length][];
			for (int j=0;j<operands.Length;j++)
			splitedvars[j] = operands[j].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
			
				
				int StartIndex = -1;
				int EndIndex = -1;
				
				for (int j=0; j<operands.Length;j++)
				{
				
				for (int k=0;k<splitedvars[j].Length;k++)
				{
					if (!splitedvars[j][k].Contains("(")&&!splitedvars[j][k].Contains(")")&&
					    splitedvars[j][k]==Common.First().Key)  // if we have a match
					{
					if (operands[j] ==Common.First().Key)  // if one left alone variable
					splitedvars[j][k] = "1";
					else
					splitedvars[j][k] = "";  // kill the sheet we are trying to factorize
					if (StartIndex==-1)  // if the first time
					{
					StartIndex = j;
					EndIndex = j;
					}
					else
					{
					EndIndex++;  // if not first time
					}
					
					}
					

					
				  }
				
				}  // for operands loop
				
				
				if (StartIndex!=-1)  // if founded something
				{
				
				string Constructed   = Common.First().Key+"*(";
				 for (int newk=StartIndex;newk<=EndIndex;newk++)
				 {
				Constructed += JoinStrings(splitedvars[newk], "*");
				 if (newk!=EndIndex)
				 Constructed += "+";
				 }
				Constructed +=  ")";
				
				splitedvars[StartIndex][0] = Constructed;  // the first item is our factorized thing
				
				for (int lame=1;lame<splitedvars[StartIndex].Length;lame++)
				splitedvars[StartIndex][lame] = "";
				
				 for (int newk=StartIndex+1;newk<=EndIndex;newk++)
				 {
				for (int lame=0;lame<splitedvars[newk].Length;lame++)
				splitedvars[newk][lame] = "";
				 }
				
				}
				
				for (int j=0;j<operands.Length;j++)
				operands[j] = JoinStrings(splitedvars[j], "*");

				equations[i] = JoinStrings(operands, "+");
			
			}  // end of IMPORTANT if (OperandsCount>=Common.First().Value)
			
			}
			
			
			}  // if 
			
			}  // for each equation end
			
			checkBox1.Checked = OLD_Resinsert;
			
			if (!checkBox1.Checked)  // if not Reinsert * selected
			{
			
			for (int i=0;i<equations.Length;i++)
			equations[i] = equations[i].Replace("*", "");
			
			}
			
			// Finnaly done
			if (textBox1.Text!=""&&File.Exists(textBox1.Text))
			{
			string OutFile = Path.GetFileNameWithoutExtension(textBox1.Text)+"_products"+Path.GetExtension(textBox1.Text);
			
			using(StreamWriter writetext = new StreamWriter(OutFile))
			{
			for (int i=0;i<equations.Length;i++)
			{
				
			writetext.WriteLine(equations[i]);
			writetext.WriteLine();
			}
			}
			
			label3.Text = "Products saved to file. Done.";
			}
			else
			{
			textBox3.Text  = "";  // remove existing text
			for (int i=0;i<equations.Length;i++)
			{
			textBox3.Text  += equations[i]+Environment.NewLine;
			}
			}
			

			
		}
		
		
		void SimplificateSystemVarReduction(string[] equations)
		{		
		string[] firstPart = null;
		string[] secondPart = null;
		
		int DecreaseCount = 0;
		int ToSimpWithIndex = -1;
		int common = 0;
		int NewLen = 0;
		int Reduction = 0;
		
		bool WasSimplificated = false;
		do
		{
		WasSimplificated = false;
		for (int i=0;i<equations.Length;i++)
		{
			firstPart = equations[i].Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
			
			DecreaseCount = 0;
			ToSimpWithIndex = -1;
			
			for (int j=0;j<equations.Length;j++)
			{
				if (i==j) continue;
				
				int HowManyInitial = BilinearCount(equations[i]); //StringExtension.Occurrences(GetUsedVariabiles(equations[i]), ',');
				string Joined_equation = equations[i]+"+"+equations[j];  // we add to curent equation that
				Joined_equation= RemoveEquationDuplicates(Joined_equation);
				
				int HowManyAfter = BilinearCount(Joined_equation);
//				StringExtension.Occurrences(GetUsedVariabiles(Joined_equation), ',');
				
				Reduction = HowManyAfter-HowManyInitial;  // smalller is better
				if ((ToSimpWithIndex==-1)||Reduction<DecreaseCount)
				{  // if first time or current value smaller then previous
						
				if (HowManyAfter<=HowManyInitial)
				{
					DecreaseCount = Reduction;
					ToSimpWithIndex = j;
				}
				
					
				
				}
				
				if (NewLen==firstPart.Length)  // we could have something else
				{
				int die = 1;
				}
					
				
			}
			
			if (ToSimpWithIndex!=-1)
			{
			
			int HowManyInitial = StringExtension.Occurrences(GetUsedVariabiles(equations[i]), ',');
			string equationBak = equations[i] ;
			
				equations[i] += "+"+equations[ToSimpWithIndex];  // we add to curent equation that
				equations[i] = RemoveEquationDuplicates(equations[i]);
				
				int HowManyAfter = StringExtension.Occurrences(GetUsedVariabiles(equations[i]), ',');
				if (HowManyAfter<=HowManyInitial)
				WasSimplificated = true;
				else
				equations[i] = equationBak;
			}
							
		}
		}
		while (WasSimplificated);
		
		}
	}
}
