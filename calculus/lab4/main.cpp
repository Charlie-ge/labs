#include <iostream>
#include <math.h>
using namespace std;

// f(x) = arctg(3+2x^3)

const double n = 10;
double h = 1 / n;

double Func(double x) {
	return atan(3 + 2 * pow(x, 3));
}

double N_Order(int p, int i, double** A) {
	double ti, fn;
	if (p == 0) {
		ti = (i - 1) * h;
		fn = Func(ti);

		if (A[p][i - 1] == 0)
			A[p][i - 1] = fn;
		return fn;
	}
	else {
		fn = (N_Order(p - 1, i + 1, A) - N_Order(p - 1, i, A)) / (h * p);

		if (A[p][i - 1] == 0)
			A[p][i - 1] = fn;

		return fn;
	}
}

double Newton(int i, double tj, double** A) {
	if (i == 0)
		return A[0][0];
	double Nn, ti, Mul = 1;

	for (int k = 1; k <= i; k++) {
		ti = (k - 1) * h;
		Mul *= (tj - ti);
	}

	Nn = A[i][0] * Mul;

	return Nn + Newton(i - 1, tj, A);
}

void main() {
	setlocale(LC_ALL, "Russian");
	int k;
	double** A = new double*[n + 1];
	for (int i = 0; i < n + 1; i++) {
		A[i] = new double[n + 1];
		for (int j = 0; j < n + 1; j++) {
			A[i][j] = 0;
		}
	}

	N_Order(n, 1, A);

	for (int i = 0; i < n + 1; i++) {
		cout << "f(t" << i + 1 << ") = " << A[0][i] << "\n";
	}
	cout << "\n";

	for (int i = 1; i < n + 1; i++) {
		for (int j = 0; j < n + 1; j++) {
			if (A[i][j] != 0) {
				cout << i << " f(";
				for (k = j + 1; k < i + j + 1; k++)
					cout << "t" << k << ", ";
				cout << "t" << k << ") = " << A[i][j] << "\n";
			}
		}
		cout << "\n";
	}

	double tj, EpsMax, EpsAver, EpsSum = 0;
	double* Nn = new double[n];
	double* Eps = new double[n];

	for (int j = 1; j < n + 1; j++) {
		tj = (j - 0.5) * h;
		Nn[j - 1] = Newton(n, tj, A);
		cout << "Nn (tj" << j << ") = " << Nn[j - 1] << "\n";

		Eps[j - 1] = abs(Func(tj) - Nn[j - 1]);
		cout << "Eps " << j << " = " << Eps[j - 1] * 1000000 << "*10^(-6)\n";
	}

	cout << "\n";

	EpsMax = abs(Eps[0]);

	for (int i = 1; i < n; i++) {
		if (abs(Eps[i]) > EpsMax)
			EpsMax = abs(Eps[i]);
	}

	for (int i = 0; i < n; i++)
		EpsSum += pow(Eps[i], 2);

	EpsAver = sqrt(h * EpsSum);

	cout << "Максимальная погрешность: " << EpsMax * 1000000 << "*10^(-6)\n";
	cout << "Среднеквадратичная погрешность: " << EpsAver * 1000000 << "*10^(-6)\n";
}
