#include <iostream>
#include <Windows.h> 
using namespace std;
const int n = 4;
int i, j, m, k;
double aa, bb;

double Tau = 0.24, Epsilon = 0.01, Q = 0.75;

double u[n];

double A[n][n + 1] = {  //  Последний столбец - правая часть системы
                {2.974, 0.347, 0.439, 0.123, 0.381},
                {0.242, 2.895, 0.412, 0.276, 0.721},
                {0.249, 0.378, 3.791, 0.358, 0.514},
                {0.387, 0.266, 0.431, 4.022, 0.795}
};

double E[n][n] = {
    {1, 0, 0, 0},
    {0, 1, 0, 0},
    {0, 0, 1, 0},
    {0, 0, 0, 1}
};

bool stopIteration(double u[], double res[], int count) { //Критерий остановки итераций
    double sum = 0;
    double dif[n];

    for (i = 0; i < n; i++) {
        dif[i] = u[i] - res[i];
    }

    cout << "На " << count << " итерации: \n";
    for (i = 0; i < n; i++) {
        cout << "вектор e[" << i << "] = " << dif[i] << "\n";
    }

    for (i = 0; i < n; i++) {
        sum += dif[i];
    }

    if (sum > (1 - Q) * Epsilon)
        cout << "Условие остановки sum < (1 - Q)* Epsilon не выполняется, продолжаем\n";
    else
        cout << "Условие остановки sum < (1 - Q)* Epsilon выполнено, прекращаем\n";

    return (sum > (1 - Q)* Epsilon);
}

int main()
{
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);

    cout << "Изначальная матрица:\n";
    for (i = 0; i < n; i++) {
        for (j = 0; j < n; j++)
            cout << A[i][j] << " ";
        cout << "\n";
    }

    cout << "Вектор F:\n";
    for (i = 0; i < n; i++)
        cout << A[i][4] << " ";

    cout << "\nTau: " << Tau << "\n\n";
    
    while (1) {
        int choice;
        cout << "1 - метод Гаусса\n2 - простая итерация\n0 - Выход\n";
        cin >> choice;
        switch (choice)
        {

        case 1: {

            for (k = 0; k < n; k++)  //  Поиск максимального элемента в первом столбце
            {
                aa = abs(A[k][k]);
                i = k;
                for (m = k + 1; m < n; m++)
                    if (abs(A[m][k]) > aa)
                    {
                        i = m;
                        aa = abs(A[m][k]);
                    }

                if (aa == 0)   //  Проверка на нулевой элемент
                {
                    cout << "Система не имеет решений" << endl;
                }

                if (i != k)  //  Перестановка i-ой строки, содержащей главный элемент k-ой строки
                {
                    for (j = k; j < n + 1; j++)
                    {
                        bb = A[k][j];
                        A[k][j] = A[i][j];
                        A[i][j] = bb;
                    }
                }
                aa = A[k][k];  //  Преобразование k-ой строки (Вычисление масштабирующих множителей)
                A[k][k] = 1;
                for (j = k + 1; j < n + 1; j++)
                    A[k][j] = A[k][j] / aa;
                for (i = k + 1; i < n; i++)  //  Преобразование строк с помощью k-ой строки
                {
                    bb = A[i][k];
                    A[i][k] = 0;
                    if (bb != 0)
                        for (j = k + 1; j < n + 1; j++)
                            A[i][j] = A[i][j] - bb * A[k][j];
                }
            }

            for (i = n - 1; i >= 0; i--)   //  Нахождение решений СЛАУ
            {
                u[i] = 0;
                aa = A[i][n];
                for (j = n; j > i; j--)
                    aa = aa - A[i][j] * u[j];
                u[i] = aa;
            }

            cout << "Решение системы:" << endl;  //  Вывод решений
            for (i = 0; i < n; i++)
            {
                cout << "u[" << i + 1 << "]=" << u[i] << endl;
            }
            break;
        }


        case 2: {
            
            double A[n][n + 1] = {  //  Последний столбец - правая часть системы
                {2.974, 0.347, 0.439, 0.123, 0.381},
                {0.242, 2.895, 0.412, 0.276, 0.721},
                {0.249, 0.378, 3.791, 0.358, 0.514},
                {0.387, 0.266, 0.431, 4.022, 0.795}
            };
            double u[n] = { 0, 0, 0, 0 };
            double B[n][n], F[n];
            int count = 0;

            cout << "Матрица В:\n";
            for (i = 0; i < n; i++) { // матрица В
                for (j = 0; j < n; j++) {
                    B[i][j] = E[i][j] - Tau * A[i][j];
                    cout << B[i][j] << " ";
                }
                cout << "\n";
            }
            for (i = 0; i < n; i++) { // вектор F
                F[i] = Tau * A[i][n];
            }

            double tempBn[n] = { 0, 0, 0, 0 }; 
            for (i = 0; i < n; i++) {
                tempBn[i] = 0;
                for (j = 0; j < n; j++) {
                    tempBn[i] += abs(B[j][i]); 
                }
            }
            double Bn = -10;
            for (int i = 0; i < n; i++) { // норма матрицы В
                if (Bn < abs(tempBn[i]))
                    Bn = abs(tempBn[i]);
            }

            cout << "Норма матрицы B = " << Bn << "\n";


            double Res[n];
            do {                        // Итерации
                for (i = 0; i < n; i++) {
                    Res[i] = u[i];
                    u[i] = 0;
                }


                for (i = 0; i < n; i++) {
                    for (j = 0; j < n; j++) {
                        u[i] += B[i][j] * Res[j];
                    }
                    u[i] += F[i];
                }
                count++;

            } while (stopIteration(u, Res, count));

            cout << "\nОтвет:\n";
            for (i = 0; i < n; i++)
                cout << "u[" << i+1 << "]=" << u[i] << "\n";
            cout << "Количество итераций: " << count << "\n";
            break;
        }
        case 0: 
            return 0;
        default:
            break;
        }
    }
    
} 
