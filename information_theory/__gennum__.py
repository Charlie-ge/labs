import random


def __gennum__():
    nums = []
    for i in range(100):
        temp = []
        total = 100
        for i in range(9):
            val = random.randint(1, int(total/4))
            total -= val
            val = val / 100
            temp.append(val)
        temp.append(total/100)
        nums.append(temp)

    for i in nums:
        print(sum(i))

    print(nums)
