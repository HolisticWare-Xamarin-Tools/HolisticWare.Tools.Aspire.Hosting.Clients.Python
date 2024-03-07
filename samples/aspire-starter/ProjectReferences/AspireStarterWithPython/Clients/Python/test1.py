#!/usr/bin/env python3
# https://www.w3schools.com/python/python_ml_train_test.asp

# brew install python-matplotlib

import numpy
import sklearn
import matplotlib.pyplot as plt

from sklearn.metrics import r2_score

numpy.random.seed(2)

x = numpy.random.normal(3, 1, 100)
y = numpy.random.normal(150, 40, 100) / x

plt.scatter(x, y)
# plt.show()
print('generating image 1.data.png')
plt.savefig('./1.data.png')


train_x = x[:80]
train_y = y[:80]

test_x = x[80:]
test_y = y[80:]

plt.scatter(train_x, train_y)
# plt.show()
print('generating image 2.1.train.png')
plt.savefig('./2.1.train.png')


plt.scatter(test_x, test_y)
# plt.show()
print('generating image 2.2.test.png')
plt.savefig('./2.2.test.png')

mymodel = numpy.poly1d(numpy.polyfit(train_x, train_y, 4))

myline = numpy.linspace(0, 6, 100)

plt.scatter(train_x, train_y)
plt.plot(myline, mymodel(myline))
# plt.show()
print('generating image 3.trained-model.png')
plt.savefig('./3.trained-model.png')


r2 = r2_score(train_y, mymodel(train_x))

print(r2)

r2 = r2_score(test_y, mymodel(test_x))

print(r2)