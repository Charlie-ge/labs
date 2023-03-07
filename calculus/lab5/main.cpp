#include <iostream>
#include <math.h>
using namespace std;

const double a = 0, b = 1.6, e = 2.7182;
const double Eps = 0.001;

double f(double x)
{
	return pow(x, 2) * pow(2.7182, -2 * x);
}


double integral(double x) {
	return -0.25 * pow(e, (-2 * x)) * (2 * pow(x, 2) + 2 * x + 1);
}


double TrapezFunc(int n)
{
    double thau = (b - a) / n;
    double summ = 0;
    for (int i = 0; i < n+1; i++)
    {
        if (i == 0 || i == n)
            summ += f(a + thau * i);
        else 
            summ += f(a + thau * i) * 2;
    }

    return thau / 2 * summ;
}


double Simpson(int n) {
    double thau = (b - a) / n;
    double summ = 0;
    for (int i = 0; i < n+1; i++) {
        if (i == 0 || i == n) {
            summ += f(a + thau * i);
        }
        else
            summ += (i % 2 == 0 ? 2 : 4) * f(a + thau * i);
    }

    return thau / 3 * summ;
}


int main()
{
    setlocale(LC_ALL, "Russian");
	double NewtLeb = integral(b) - integral(a);
	cout << NewtLeb << "\n\n";

    double trapez, dif = 1;
    int i = 1;

    while (dif >= Eps) {
        i++;
        trapez = TrapezFunc(i);
        dif = abs(NewtLeb - trapez);
        cout << "N = " << i << ": " << trapez <<  ", погрешность: " << dif << "\n";
    }

    cout << "Число узлов для метода трапеции:" << i+1 << "\n";
    cout << "Ответ по методу трапеций: " << trapez << "\n\n";

    i = 2;
    int points = 0;
    double simp;
    dif = 1;
    while(dif >= Eps) {
        simp = Simpson(i);
        dif = abs(NewtLeb - simp);
        cout << "N = " << i << ": " << simp << ", погрешность: " << dif << "\n";
        i += 2;
        points++;
    }

    cout << "Число узлов для метода Симпсона:" << points*2+1 << "\n";
    cout << "Ответ по методу Симпсона: " << simp << "\n";
}
