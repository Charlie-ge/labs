import math
import numpy as np
import random

N = 128

nu = math.ceil(math.log(N, 2))
nk = math.ceil(math.log(nu + 1 + math.log(nu + 1, 2), 2))
n = nu + nk
d = math.ceil(math.log2(n + 1 + math.log2(n + 1)))

print("n(и) = ", nu, "\nn(к) = ", nk, "\nn = ", n, "\nd = ", d)

def switchchar(arr, pos, char):
    temp = list(arr)
    temp[pos] = char
    arr = "".join(temp)
    return arr


def Encode(message):
    lgk = nk * '0'
    lgk2 = [0, 0, 0, 0]
    for i in range(nu):
        if message[i] == '1':
            for j in range(nk):
                lgk2[j] = controlMatrix[i][j]
                if lgk[j] == str(lgk2[j]):
                    lgk = switchchar(lgk, j, '0')
                else:
                    lgk = switchchar(lgk, j, '1')
    return message + lgk

def makeerror(arr):
    err = random.randint(1, nu)
    print(f"Ошибка создана в {err + 1} бите")
    if arr[err] == '0':
        arr = switchchar(arr, err, '1')
    else:
        arr = switchchar(arr, err, '0')

    print("Код с ошибкой:", arr)
    return arr


def calcSyndrome(arr):
    synd = ''
    for i in range(nk):
        sum = 0
        for j in range(n):
            if H[i][j] == 1:
                if arr[j] == '1':
                    sum += 1
        synd += str(sum % 2)
    bc = ''

    for i in range(n):
        temp = ''
        for j in range(nk):
            temp += str(H[j][i])

        if temp == synd:
            print("Ошибка на позиции ", i + 1)
            if arr[i] == '1':
                bc += '0'
            else:
                bc += '1'
        else:
            bc += arr[i]
    print(bc[:-4])


Matrix = np.eye(nu, dtype=int)
controlMatrix = np.array([[1, 1, 1, 1],
                          [1, 1, 1, 0],
                          [1, 1, 0, 1],
                          [1, 0, 1, 1],
                          [1, 0, 0, 1],
                          [0, 1, 1, 1],
                          [0, 1, 1, 0]])

A = np.transpose(controlMatrix)

B = np.eye(nk, dtype=int)
H = np.hstack((A, B))
Matrix = np.hstack((Matrix, controlMatrix))
print(H)
print(Matrix)

for k in range(10):
    data = bin(random.randint(1, N))
    data = data[2:]

    if (len(data) < nu):
        r = nu - len(data)
        data = r * '0' + data

    print("Сообщение: ", data)

    arr = Encode(data)
    print("Зашифрованное сообщение: ", arr)
    arr = makeerror(arr)
    calcSyndrome(arr)
    print()
