import math
import random

N = 128

nu = math.ceil(math.log(N, 2))
nk = math.ceil(math.log(nu + 1 + math.log(nu + 1, 2), 2))
n = nu + nk

print("n(и) = ", nu, "\nn(к) = ", nk, "\nn = ", n)


def checkfor2(i):
    return i & (i - 1) == 0


def switchchar(arr, pos, char):
    temp = list(arr)
    temp[pos] = char
    arr = "".join(temp)
    return arr


def calcRedundantBits(m):
    for i in range(m):
        if (2 ** i >= m + i + 1):
            return i


def posRedundantBits(data, r):
    j = 0
    k = 1
    m = len(data)
    res = ''

    for i in range(1, m + r + 1):
        if not (i & (i - 1)):
            res = res + '0'
        else:
            res = res + data[j]
            j += 1

    return res


def calcParityBits(arr, r):
    n = len(arr)
    t = 0
    for i in range(nk):
        temp2 = 0
        twod = 2 ** i
        for j in range(twod - 1, n, twod * 2):
            temp = 0
            for k in range(j, (j + twod), 1):
                if k >= n:
                    continue
                temp = temp + int(arr[k])
                temp = temp % 2
            temp2 += temp
            temp2 %= 2
        arr = switchchar(arr, twod - 1, str(temp2))

    for i in range(n):
        t += int(arr[i])
    t %= 2
    arr += str(t)
    return arr


def calcSyndrome(arr, nr):
    n = len(arr) - 1
    check = []
    err, synd = 0, 0
    for i in range(nk):
        temp2 = 0
        twod = 2 ** i
        switchchar(arr, twod-1, '0')
        for j in range(twod - 1, n, twod * 2):
            temp = 0
            for k in range(j, (j + twod), 1):
                if k >= n:
                    continue
                temp = temp + int(arr[k])
                temp = temp % 2
            temp2 += temp
            temp2 %= 2
        check.append(temp2)
        synd += temp2
        err += twod * check[i]
    return synd, err - 1

def controlbits(arr):
    control = 0
    for i in range(len(arr) - 1):
        control += int(arr[i])

    control %= 2

    if control == int(arr[-1]):
        return True
    else:
        return False


def makeerror(arr):
    num = random.randint(0, 2)
    for i in range(num):
        err = random.randint(1, nu)
        print(f"Ошибка создана в {err} бите")
        if arr[err] == '0':
            arr = switchchar(arr, err, '1')
        else:
            arr = switchchar(arr, err, '0')

    if num != 0:
        print("Код с ошибкой:", arr)

    return arr


for i in range(10):
    data = bin(random.randint(1, N))
    data = data[2:]

    print("Сообщение: ", data)

    if (len(data) < nu):
        r = nu - len(data)
        data = r * '0' + data

    arr = posRedundantBits(data, nk)

    arr = calcParityBits(arr, nk)

    print("Зашифрованное сообщение: " + arr)

    arr = makeerror(arr)
    synd, err = calcSyndrome(arr, nk)
    if controlbits(arr):
        if synd == 0:
            print("В переданном сообщении нет ошибки")
            decode = ''
            for i in range(1, n):
                if not checkfor2(i+1):
                    decode = decode + arr[i]
            print(decode)
        else:
            print("В коде двойная ошибка, декодировать нельзя")
    else:
        print("Ошибка на", err, "позиции")
        if arr[err] == '0':
            arr = arr[:err] + '1' + arr[err + 1:]
        else:
            arr = arr[:err] + '0' + arr[err + 1:]
        print("Исправленный код: ", arr)
        decode = ''
        j = 0
        for i in range(1, n):
            if not checkfor2(i + 1):
                decode = decode + arr[i]
        print(decode)
    print()
