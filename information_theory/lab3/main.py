import math


class  shan :
    def __init__(self) -> None:
        # for storing symbol
        self.sym=''
        # for storing probability or frequency
        self.pro=0.0
        self.arr=[0]*20
        self.top=0
p=[shan() for i in range(33)]


# A Huffman Tree Node
class node:
    def __init__(self, freq, symbol, left=None, right=None):
        # frequency of symbol
        self.freq = freq

        # symbol name (character)
        self.symbol = symbol

        # node left of current node
        self.left = left

        # node right of current node
        self.right = right

        # tree direction (0/1)
        self.huff = ''

# utility function to print huffman
# codes for all symbols in the newly
# created Huffman tree

symbols = []
codes = []
probs = []
def printNodes(node, val=''):
    newVal = val + str(node.huff)

    # if node is not an edge node
    # then traverse inside it
    if(node.left):
        printNodes(node.left, newVal)
    if(node.right):
        printNodes(node.right, newVal)

        # if node is edge node then
        # display its huffman code
    if(not node.left and not node.right):
        print(f"\t{node.symbol} \t{node.freq}\t {newVal}")
        symbols.append(node.symbol)
        codes.append(newVal)
        probs.append(node.freq / len(text))




def shannon(l, h, p):
    pack1 = 0
    pack2 = 0
    diff1 = 0
    diff2 = 0
    if ((l + 1) == h or l == h or l > h):
        if (l == h or l > h):
            return
        p[h].top += 1
        p[h].arr[(p[h].top)] = 0
        p[l].top += 1
        p[l].arr[(p[l].top)] = 1

        return

    else:
        for i in range(l, h):
            pack1 = pack1 + p[i].pro
        pack2 = pack2 + p[h].pro
        diff1 = pack1 - pack2
        if (diff1 < 0):
            diff1 = diff1 * -1
        j = 2
        k = 0
        while (j != h - l + 1):
            k = h - j
            pack1 = pack2 = 0
            for i in range(l, k + 1):
                pack1 = pack1 + p[i].pro
            for i in range(h, k, -1):
                pack2 = pack2 + p[i].pro
            diff2 = pack1 - pack2
            if (diff2 < 0):
                diff2 = diff2 * -1
            if (diff2 >= diff1):
                break
            diff1 = diff2
            j += 1

        k += 1
        for i in range(l, k + 1):
            p[i].top += 1
            p[i].arr[(p[i].top)] = 1

        for i in range(k + 1, h + 1):
            p[i].top += 1
            p[i].arr[(p[i].top)] = 0

        # Invoke shannon function
        shannon(l, k, p)
        shannon(k + 1, h, p)


def sortByProbability(n, p):
    for i in range(n-1):
            for j in range(n-i-1):
                if p[j].pro > p[j+1].pro:
                    p[j].pro, p[j+1].pro = p[j+1].pro, p[j].pro
                    p[j].sym, p[j+1].sym = p[j+1].sym, p[j].sym



def display(n, p):
    print("\n\n\n\tSymbol\tProbability\tCode",end='')
    for i in range(n - 1,-1,-1):
        print("\n\t", p[i].sym, "\t\t",end='')
        for j in range(p[i].top+1):
            print(p[i].arr[j],end='')


def countstuff(prob, cod):
    lcp = 0
    for i in range(32):
        l = len(cod[i]) * prob[i]
        lcp += l

    H_max = math.log(32, 2)
    Kcc = H_max / lcp

    Koe = 0
    for i in range(32):
        Koe += prob[i] * math.log(prob[i], 2)
    Koe *= -1

    Koe = Koe / lcp

    print("Средняя длина символа исходного алфавита: Lcp = ", lcp)
    print("Коэффициент статистического сжатия: Kcc = ", Kcc)
    print("Коэффициент относительной эффективности: Коэ = ", Koe)


file = open('text.txt', encoding='utf-8')
dictionary = ["а", "б", "в", "г", "д", "е", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ь", "ы", "э", "ю", "я", " "]
text = file.read().lower()
file.close()
for i in text:
    if i == 'ъ':
        text = text.replace(i, '')
    if i.isalpha() or i == ' ':
        continue
    else:
        text = text.replace(i, '')


H_x = 4.369995076
H_xy = 3.5835739

Dp = 1 - (H_x / math.log(32, 2))
Ds = 1 - (H_xy / H_x)
Dsp = Ds + Dp - Dp * Ds

print(f"Dp = {Dp}\nDs = {Ds}\nD = {Dsp}")

n = 32
prob = []
print(len(text))
for i, j in enumerate(dictionary):
    p[i].pro = text.count(j) / (1.27 * len(text))
    prob.append(p[i].pro)
    p[i].sym += j

sortByProbability(n, p)

for i in range(n):
    p[i].top = -1

    # Find the shannon code
shannon(0, n - 1, p)

# Display the codes
display(n, p)

print()

res = []
for i in range(n - 1, -1, -1):
    temp = ''
    for j in range(p[i].top + 1):
        temp += str(p[i].arr[j])
    res.append(temp)
res.reverse()


bin_text = text
for i in range(n - 1, -1, -1):
    bin_text = bin_text.replace(p[i].sym, res[i])


print(bin_text)

result = ''
temp = ''
for i in bin_text:
    temp += i
    for j in range(32):
        if res[j] == temp:
            result += p[j].sym
            temp = ''

print(result)

# list containing unused nodes
nodes = []

# converting characters and frequencies
# into huffman tree nodes
for x in dictionary:
    nodes.append(node(text.count(x), x))

while len(nodes) > 1:
    # sort all the nodes in ascending order
    # based on theri frequency
    nodes = sorted(nodes, key=lambda x: x.freq)

    # pick 2 smallest nodes
    left = nodes[0]
    right = nodes[1]

    # assign directional value to these nodes
    left.huff = 0
    right.huff = 1

    # combine the 2 smallest nodes to create
    # new node as their parent
    newNode = node(left.freq + right.freq, left.symbol + right.symbol, left, right)

    # remove the 2 nodes and add their
    # parent as new node among others
    nodes.remove(left)
    nodes.remove(right)
    nodes.append(newNode)


printNodes(nodes[0])

bin_text = text
for i in range(n):
    bin_text = bin_text.replace(symbols[i], codes[i])


print(bin_text)

result = ''
temp = ''
for i in bin_text:
    temp += i
    for j in range(32):
        if codes[j] == temp:
            result += symbols[j]
            temp = ''
print(result)

print("Значения для метода Шеннона-Фано:")
countstuff(prob, res)
print("\nЗначения для метода Хаффмана:")
countstuff(probs, codes)
