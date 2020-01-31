using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePerceptron1 {
    class Program {

        static void Main(string[] args) {

            Neuron neuron = new Neuron();
            List<TrainingSet> trainingSets = new List<TrainingSet>();

            for (int i = 0; i < 20; i++) {
                trainingSets.Add(new TrainingSet(new double[2] { 0, 0 }, 0));
                trainingSets.Add(new TrainingSet(new double[2] { 0, 1 }, 0));
                trainingSets.Add(new TrainingSet(new double[2] { 1, 0 }, 0));
                trainingSets.Add(new TrainingSet(new double[2] { 1, 1 }, 1));
            }

            for (int i = 0; i < trainingSets.Count(); i++) {
                if (trainingSets[i].Target == neuron.Learn(trainingSets[i])) {
                    Console.Write("O");
                }
                else {
                    Console.Write("X");
                }
                Console.WriteLine("");
            }

        }
    }


    class TrainingSet {
        public double[] Features { get; private set; }
        public double Target { get; private set; }

        public TrainingSet(double[] features, double target) {
            Features = features;
            Target = target;
        }

    }




    class Neuron {
        private double[] weight = new double[1024];
        private double bias = 0;
        private double learningRate = 0.9;
        private int actFunc = 0;  //0->step  1->sigmoid  2->ReLU

        public Neuron() {
        }


        public double Learn(TrainingSet trainingSet) {
            int length = trainingSet.Features.Length;




            double sum = 0;
            for (int i = 0; i < length; i++) {
                sum += trainingSet.Features[i] * weight[i];
            }
            sum += bias;
            double result = 0;
            switch (actFunc) {
                case 0:
                    result = Step(sum);
                    break;
                case 1:
                    result = Sigmoid(sum);
                    break;
                case 2:
                    result = ReLU(sum);
                    break;
            }


            Console.Write("    ");
            Console.Write(string.Format("{0,-3}", result));
            Console.Write("    ");


            switch (actFunc) {
                case 0:
                    for (int i = 0; i < length; i++) {
                        weight[i] = weight[i] + (learningRate * (trainingSet.Target - result) * trainingSet.Features[i]);
                    }
                    bias = bias + (learningRate * (trainingSet.Target - result));
                    break;
                case 1:
                    for (int i = 0; i < length; i++) {
                        weight[i] = weight[i] + (learningRate * (trainingSet.Target - result) * SigmoidDeriv(result) * trainingSet.Features[i]);
                    }
                    bias = bias + (learningRate * (trainingSet.Target - result) * SigmoidDeriv(result));
                    break;
                case 2:
                    for (int i = 0; i < length; i++) {
                        weight[i] = weight[i] + (learningRate * (trainingSet.Target - result) * trainingSet.Features[i]);
                    }
                    bias = bias + (learningRate * (trainingSet.Target - result));
                    break;
            }

            return result;
        }


        public double BackPropagation() {
            return 0;
        }


        private double ReLU(double x) {
            if (x >= 0) {
                return x;
            }
            else {
                return 0;
            }
        }

        private double Sigmoid(double x) {
            return 1 / (1 + Math.Exp(-x));
        }


        private double SigmoidDeriv(double y) {
            return y * (1 - y);
        }
        

        private double Step(double x) {
            if (x >= 0) {
                return 1;
            }
            else {
                return 0;
            }
        }


    }



}
