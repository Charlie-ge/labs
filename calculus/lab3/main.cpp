#include <iostream>
#include <Windows.h>
#include <math.h>
using namespace std;

//f(x) = 1.5 - 0.4 * sqrt(x^3) - 0.5 * ln(x)

const float E = 0.01, a = 2, b = 3;
const float x0 = (a + b) / 2;

float funcX(float x) {
	return 1.5 - 0.4 * sqrtf(pow(x, 3)) - 0.5 * log(x);
}

float derfx(float x) {
	return -0.6 * sqrtf(pow(x, 3)) / x - 0.5 / x;
}


bool checkFaFb() {
	float funcA = funcX(a);
	float funcB = funcX(b);
	cout << "f(a)f(b) = " << funcA * funcB << "\n";
	return funcA * funcB < 0;
}


bool checkFxFFx() {
	float funcX0 = derfx(x0);
	float funcXder = 0.5 / (x0 * (x0 + 1)) - (0.6 * (-sqrtf(pow(x0, 3)) * x0 - sqrtf(pow(x0, 3)) + sqrtf(pow((x0 + 1), 3)) * x0)) / (x0 * (x0 + 1));
	cout << "f'(x0) = " << funcX0 << "\nf''(x0) = " << funcXder << "\n";
	cout << "f'(x0)f''(x0) = " << funcX0 * funcXder << "\n";
	return funcX0 * funcXder > 0;
}

void main() {
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
	
	if (checkFaFb() and checkFxFFx())
		cout << "Условия выполнены\n";
	else
		cout << "Условия не выполнены\n";
	float x, x1 = x0;
	int count = 0;
	do {
		x = x1;
		x1 = x - funcX(x) / derfx(x);
		count++;
		cout << "x[" << count << "] = " << x1 << "\n";
	} while (abs(x1 - x) >= E);

	cout << "x = " << x1 << "\n";
	cout << "Количество итераций: " << count << "\n";
}
