#include <map>
#include <iostream>
#include <Windows.h>
using namespace std;

const int n = 9, p = 4;
double A[p + 1][p + 1] = {
    {0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0} };
double F[p + 1] = { 0, 0, 0, 0, 0 };
int aa;
double temp, bb;

double x[n] = { 0.092, 0.448, 0.803, 1.159, 1.515, 1.871, 2.227, 2.582, 2.938 };
double func[n] = { 2.161, 1.824, 1.214, 0.431, -0.792, -2.004, -3.635, -5.333, -7.438 };
double u[p + 1] = { 0, 0, 0, 0, 0 };
double res[n] = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };


/*map<double, double> InitialValues{
    {0.092, 2.161},
    {0.448, 1.824},
    {0.803, 1.214},
    {1.159, 0.431},
    {1.515, -0.792},
    {1.871, -2.004},
    {2.227, -3.635},
    {2.582, -5.333},
    {2.938, -7.438}
};*/


void GausMethod() {
    for (int i = 0; i < p+1; i++) //Построчное преобразование методом Гаусса
    {
        bb = abs(A[i][i]);
        aa = i;

        for (int j = i + 1; j < p+1; j++) //Поиск максимального элемента в столбце
            if (abs(A[j][i]) > bb)
            {
                aa = j;
                bb = abs(A[j][i]);
            }

        if (aa != i)  //Перестановка строки, содержащей главный элемент
        {
            for (int j = i; j < p+1; j++)
            {
                temp = A[i][j];
                A[i][j] = A[aa][j];
                A[aa][j] = temp;
            }

            temp = F[i];
            F[i] = F[aa];
            F[aa] = temp;
        }

        bb = A[i][i];  //Преобразование i-ой строки (Вычисление масштабирующих множителей)
        A[i][i] = 1;

        for (int j = i + 1; j < p+1; j++)
            A[i][j] = A[i][j] / bb;

        F[i] = F[i] / bb;

        for (aa = i + 1; aa < p + 1; aa++)  //Преобразование остальных строк
        {
            temp = A[aa][i];
            A[aa][i] = 0;
            if (temp != 0)
            {
                for (int j = i + 1; j < p + 1; j++)
                    A[aa][j] = A[aa][j] - temp * A[i][j];

                F[aa] = F[aa] - temp * F[i];
            }
        }
    }
 
    for (aa = p; aa >= 0; aa--)   //Нахождение решений СЛАУ
    {
        u[aa] = 0;
        bb = F[aa];

        for (int j = p; j > aa; j--)
            bb = bb - A[aa][j] * u[j];

        u[aa] = bb;
    }

    cout << "Решение системы:" << endl;  //  Вывод решений
    for (aa = 0; aa < p+1; aa++)
    {
        cout << "u [" << aa << "] = " << u[aa] << endl;
    }
}


int main()
{
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);

    for (int i = 0; i < p+1; i++)
        for (int j = 0; j < p+1; j++)
            for (int k = 0; k < n; k++)
                A[i][j] = A[i][j] + pow(x[k], i + j);

    for (int i = 0; i < p+1; i++)
        for (int k = 0; k < n; k++)
            F[i] = F[i] + pow(x[k], i) * func[k];

    cout << "Матрица A:" << endl;
    for (int i = 0; i < p+1; i++)
    {
        for (int j = 0; j < p+1; j++)
            cout << " " << A[i][j];


        cout << "           " << F[i] <<  endl;
    }

    GausMethod();
    cout << "f(x) = " << u[0] << u[1] << "*x";
    for (int i = 2; i < p+1; i++)
    {
        cout << u[i] << "*x^" << i;

        if ((i != p) && (u[i] < 0))
            cout << "+";
    }

    /*cout << endl << "Матрица F:" << endl;
    for (int i = 0; i < l; i++)
        cout << " " << F[i];*/

    cout << endl;

    for (int k = 0; k < n; k++)
    {
        for (int i = 0; i < p+1; i++)
            res[k] = res[k] + pow(x[k], i) * u[i];

        res[k] = res[k] - func[k];
    }

    cout << "Невязки:" << endl;
    for (int i = 0; i < n; i++)
        cout << " " << abs(res[i]);
    cout << endl;

}
