using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UAM_Calculator
{
    public partial class Calculator : Form
    {
        private int unknown_count;
        
        private List<string> unknowns = new List<string>();
        private double vi, vf, a, x, t;
        private MeasurementValues mv_vi, mv_vf, mv_a, mv_x, mv_t;
        private MeasurementValues answer = new UAM_Calculator.MeasurementValues(0, "");

        private void L_answer2_Click(object sender, EventArgs e)
        {

        }

        private void L_vf_Click(object sender, EventArgs e)
        {

        }

        private void L_vi_Click(object sender, EventArgs e)
        {

        }

        private void Calculator_Load(object sender, EventArgs e)
        {

        }

        public Calculator()
        {
            mv_vi = new MeasurementValues(0, "");
            mv_vf = new MeasurementValues(0, "");
            mv_t = new MeasurementValues(0, "");
            mv_x = new MeasurementValues(0, "");
            mv_a = new MeasurementValues(0, "");
            
            unknown_count = 0;
            InitializeComponent();
        }

        private void B_Calculate_Click(object sender, EventArgs e)
        {
            unknowns.Clear();
            unknown_count = 0;
            /*
            vi = vf = a = x = t = 0;
            mv_vi.setValue(0);
            mv_vf.setValue(0);
            mv_t.setValue(0);
            mv_x.setValue(0);
            mv_a.setValue(0);*/
            execute();
            
            
        }
        private void execute()
        {
            
            if (this.T_a.Text.Length != 0) { mv_a.setValue(Convert.ToDouble(T_a.Text)); this.a = Convert.ToDouble(T_a.Text); }
            if (this.T_x.Text.Length != 0) { mv_x.setValue(Convert.ToDouble(T_x.Text)); this.x = Convert.ToDouble(T_x.Text); }
            if (this.T_vi.Text.Length != 0) { mv_vi.setValue(Convert.ToDouble(T_vi.Text)); this.vi = Convert.ToDouble(T_vi.Text); System.Diagnostics.Debug.WriteLine("SET"); }
            if (this.T_vf.Text.Length != 0) { mv_vf.setValue(Convert.ToDouble(T_vf.Text)); this.vf = Convert.ToDouble(T_vf.Text); }
            if (this.T_t.Text.Length != 0) { mv_t.setValue(Convert.ToDouble(T_t.Text)); this.t = Convert.ToDouble(T_t.Text); }

            if (this.T_a.Text.Length == 0) { mv_a.setUnknown(true); unknown_count++; unknowns.Add("a"); }
            if (this.T_t.Text.Length == 0) { mv_t.setUnknown(true); unknown_count++; unknowns.Add("t"); }
            if (this.T_vi.Text.Length == 0) { mv_vi.setUnknown(true); unknown_count++; unknowns.Add("vi"); }
            if (this.T_vf.Text.Length == 0) { mv_vf.setUnknown(true); unknown_count++; unknowns.Add("vf"); }
            if (this.T_x.Text.Length == 0) { mv_x.setUnknown(true); unknown_count++; unknowns.Add("x"); }
            System.Diagnostics.Debug.WriteLine("processed values");
            if (unknown_count == 2)
            {
                System.Diagnostics.Debug.Write("passed unknown test");
                System.Diagnostics.Debug.Write(unknowns.Contains("a"));
                System.Diagnostics.Debug.Write(unknown_count);
                for (int i = 0; i < 2; i++)
                {
                    if (unknowns[i]=="a")
                    {
                        System.Diagnostics.Debug.Write("passed here");
                        calc_a();
                        if (i == 0) { this.L_answer.Text = "Acceleration: "+answer.getValue().ToString()+" m/s/s"; }
                        if (i == 1) { this.L_answer2.Text = "Acceleration: " + answer.getValue().ToString() + " m/s/s"; }
                        //this.unknowns.Remove("a");
                    }
                    else if (unknowns[i]=="x")
                    {
                        calc_x();
                        if (i == 0) { this.L_answer.Text = "Displacement: "+answer.getValue().ToString()+" m"; }
                        if (i == 1) { this.L_answer2.Text = "Displacement: " + answer.getValue().ToString() + " m"; }
                        //this.unknowns.Remove("x");
                    }
                    else if (unknowns[i]=="vi")
                    {
                        calc_vi();
                        if (i == 0) { this.L_answer.Text = "Initial Velocity: "+answer.getValue().ToString()+" m/s"; }
                        if (i == 1) { this.L_answer2.Text = "Initial Velocity: " + answer.getValue().ToString() + " m/s"; }
                        //this.unknowns.Remove("vi");
                    }
                    else if (unknowns[i]=="vf")
                    {
                        calc_vf();
                        if (i == 0) { this.L_answer.Text = "Final Velocity: "+answer.getValue().ToString()+" m/s"; }
                        if (i == 1) { this.L_answer2.Text = "Final Velocity: " + answer.getValue().ToString() + " m/s"; }
                        //this.unknowns.Remove("vf");
                    }
                    else if (unknowns[i]=="t")
                    {
                        calc_t();
                        if (i == 0) { this.L_answer.Text = "Time: "+answer.getValue().ToString()+" s"; }
                        if (i == 1) { this.L_answer2.Text = "Time: " + answer.getValue().ToString() + " s"; }
                        //this.unknowns.Remove("t");
                    }
                }
            }
            else
            {
                this.L_answer.Text = "Please leave 2 unknowns";
                this.L_answer2.Text = "Having too many or too less given values leads to the\nequations failing due to incompatability";
            }
        }
        public void calc_vi()
        {
            if (mv_vf.getUnknown() == true)
            {
                this.answer.setValue((x - 0.5 * a * t * t) / t);
            }
            else if (mv_a.getUnknown() == true)
            {
                this.answer.setValue((2 * x) / t - vf);
            }
            else if (mv_x.getUnknown() == true)
            {
                this.answer.setValue(vf - a * t);
            }
            else if (mv_t.getUnknown() == true)
            {
                this.answer.setValue(Math.Sqrt(vf * vf - (2 * a * x)));
            }

        }
        public void calc_vf()
        {
            if (mv_vi.getUnknown() == true)
            {
                this.answer.setValue((vf - Math.Sqrt(vf * vf - (2 * a * x))) / a);
            }
            else if (mv_a.getUnknown() == true)
            {
                this.answer.setValue(2 * (x / t) - vi);
            }
            else if (mv_x.getUnknown() == true)
            {
                this.answer.setValue(vi + a * t);
            }
            else if (mv_t.getUnknown() == true)
            {
                this.answer.setValue(Math.Sqrt(vi * vi + 2 * a * x));
            }

        }
        public void calc_a()
        {
            if (mv_vf.getUnknown() == true)
            {
                answer.setValue((2 * (x - vi * t)) / (t * t));
            }
            else if (mv_vi.getUnknown() == true)
            {
                answer.setValue((vf - Math.Sqrt(vf * vf - (2 * a * x))) / t);
            }
            else if (mv_x.getUnknown() == true)
            {
                answer.setValue((vf - vi) / t);
            }
            else if (mv_t.getUnknown() == true)
            {
                System.Diagnostics.Debug.WriteLine("yeet");
                System.Diagnostics.Debug.WriteLine(vf.ToString());
                System.Diagnostics.Debug.WriteLine(vi.ToString());
                System.Diagnostics.Debug.WriteLine(x.ToString());
                answer.setValue((vf * vf - vi * vi) / (x * 2));
            }

        }
        public void calc_x()
        {
            if (mv_vf.getUnknown() == true)
            {
                answer.setValue(vi * t + 0.5 * a * t * t);
            }
            else if (mv_a.getUnknown() == true)
            {
                answer.setValue(((vf + vi) * t) / 2);
            }
            else if (mv_vi.getUnknown() == true)
            {
                answer.setValue((vf * t) - (0.5 * a * t * t));
            }
            else if (mv_t.getUnknown() == true)
            {
                answer.setValue((vf * vf - vi * vi) / (2 * a));
            }


        }
        public void calc_t()
        {
            if (mv_vf.getUnknown() == true)
            {
                answer.setValue((Math.Sqrt((2 * (x / vi)) / a)) / 2);
            }
            else if (mv_a.getUnknown() == true)
            {
                answer.setValue(x / ((vf + vi) / 2));
            }
            else if (mv_x.getUnknown() == true)
            {
                answer.setValue((vf - vi) / a);
            }
            else if (mv_vi.getUnknown() == true)
            {
                answer.setValue((vf - Math.Sqrt(vf * vf - (2 * a * x))) / a);
            }

        }

    }
}
