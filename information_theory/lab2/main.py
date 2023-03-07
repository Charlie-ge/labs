import numpy as np
import math
import xlsxwriter

matrix = np.array([[0.0014, 0.0008, 0.0019, 0.0011, 0.0098, 0.0156, 0.0022, 0.009,  0.0037, 0.0282],
                   [0.0237, 0.0011, 0.0002, 0.0034, 0.0157, 0.0133, 0.0028, 0.0001, 0.0044, 0.0102],
                   [0.0045, 0.0689, 0.0002, 0.0047, 0.0141, 0.0036, 0.0006, 0.0005, 0.0011, 0.0027],
                   [0.0015, 0.033, 0.0026, 0.0034, 0.012, 0.0006, 0.0075, 0.0003, 0.0172, 0.0009],
                   [0.0088, 0.0065, 0.001, 0.0008, 0.0107, 0.0042, 0.0023, 0.0039, 0.0004, 0.019],
                   [0.0001, 0.001, 0.016, 0.0042, 0.0009, 0.0067, 0.0097, 0.0148, 0.0015, 0.0024],
                   [0.0025, 0.0024, 0.0635, 0.0011, 0.0013, 0.0124, 0.0013, 0.0513, 0.0006, 0.0009],
                   [0.0037, 0.003, 0.0429, 0.0311, 0.0016, 0.0311, 0.0008, 0.0011, 0.0341, 0.0013],
                   [0.0011, 0.0044, 0.0003, 0.0015, 0.0223, 0.0222, 0.0014, 0.0012, 0.0252, 0.0465],
                   [0.0458, 0.0144, 0.0048, 0.0029, 0.0228, 0.015, 0.0137, 0.0127, 0.0053, 0.0051]])


def pxi(mat):
    res = []
    pi = []
    for i in mat:
        resi = []
        x = sum(i)
        pi.append(x)
        for j in i:
            resi.append(round(j / x, 4))
        res.append(resi)

    return res, pi


def chunks(lst, n):
    return [lst[i:i + n] for i in range(0, len(lst), n)]


print("Канальная матрица со стороны приемника:")
sender, pi_send = pxi(matrix)
for i in sender:
    print(i)

print("\nКанальная матрица со стороны источника:")
reciever, pi_rec = pxi(matrix.transpose())
for i in reciever:
    print(i)
print()

hxy = 0
count = 0
for i in sender:
    hxyi = 0
    for j in i:
        if j == 0:
            continue
        hxyi += j * math.log2(j)
    hxyi *= -1
    print("H(X|y", count, ") = ", hxyi)
    hxy += pi_send[count] * hxyi
    count += 1
print("H(X|Y) = ", hxy)
print()

count = 0
hyx = 0
for i in reciever:
    hyxi = 0
    for j in i:
        if j == 0:
            continue
        hyxi += j * math.log2(j)
    hyxi *= -1
    print("H(Y|x", count + 1, " = ", hyxi)
    hyx += pi_rec[count] * hyxi
    count += 1
print("H(Y|X) = ", hyx)


dictionary = ["а", "б", "в", "г", "д", "е", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "э", "ю", "я", " "]
letter_comb = []
for i in dictionary:
    for j in dictionary:
        letter_comb.append(i+j)

file = open('text.txt', encoding='utf-8')
text = file.read().lower()
file.close()

count = []
for i in dictionary:
    count.append(text.count(i))

combP = []
print(sum(count))
for i in count:
    combP.append(i / sum(count))

letter_comb = chunks(letter_comb, 32)
combP = chunks(combP, 32)

for i in letter_comb:
    print(i)
for i in combP:
    print(i)

pi_comb, pix = pxi(combP)

file = xlsxwriter.Workbook('TI2.xlsx')
worksheet = file.add_worksheet('2')
row = 100
col = 1

hxy = 0
count = 0
for i in pi_comb:
    hxyi = 0
    for j in i:
        if j == 0:
            continue
        hxyi += j * math.log2(j)
    hxyi *= -1
    print("H(X|y = ", hxyi)
    worksheet.write(row, col, hxyi)
    col += 1
    hxy += pix[count] * hxyi
    count += 1
print("H(X|Y) = ", hxy)
print()

row = 1
col = 1

for i in letter_comb:
    for j in i:
        worksheet.write(row, col, j)
        col += 1
    col = 1
    row += 1

row += 1

for i in combP:
    for j in i:
        worksheet.write(row, col, j)
        col += 1
    col = 1
    row += 1

row += 1
for i in pi_comb:
    for j in i:
        worksheet.write(row, col, j)
        col += 1
    col = 1
    row += 1

file.close()
