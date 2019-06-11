import os
import numpy as np
import matplotlib.pyplot as plt

def get_path(file):
    work_folder = os.path.abspath('../data')
    return os.path.abspath(work_folder + '/' + file)

def mean_noice(bits, num_pass, num_unique):
    mean_pass = num_pass / num_unique
    mean_shr = mean_pass + ((num_pass - mean_pass) / (2 ** bits))
    return mean_pass / mean_shr * 100

def pass_noice(bits, num_pass, num_123):
    mean_shr = num_123 + ((num_pass - num_123) / (2 ** bits))
    return num_123 / mean_shr * 100

def data_pass(num):
    xs = np.linspace(40, 2, 100)
    ys = [pass_noice(bits, num_pass, num) for bits in xs]

    xs_dot = list(range(40, 0, -2))
    ys_dot = [pass_noice(bits, num_pass, num) for bits in xs_dot]

    return [(xs, ys), (xs_dot, ys_dot)]

def data_mean():
    xs = np.linspace(40, 2, 100)
    ys = [mean_noice(bits, num_pass, num_unique) for bits in xs]

    xs_dot = list(range(40, 0, -2))
    ys_dot = [mean_noice(bits, num_pass, num_unique) for bits in xs_dot]
    
    return [(xs, ys), (xs_dot, ys_dot)]

def plot(xs, ys, xs_dot, ys_dot, color):
    plt.figure(num=1, figsize=(10, 10), dpi=80)
    plt.xlabel('Hash Size (bits)', fontsize=15)
    plt.ylabel('Hack Certainty (%)', fontsize=15)
    plt.xticks(xs_dot)
    plt.yticks(range(100, -1, -10))
    plt.grid(axis='y', linestyle='--')
    plt.plot(xs, ys, '-', color=color)
    plt.plot(xs_dot, ys_dot, 'o', color=color, label='_nolegend_')

def run(num_pass, num_unique, path):
    [(xs, ys), (xs_dot, ys_dot)] = data_pass(374825) #123456
    plot(xs, ys, xs_dot, ys_dot, 'red')

    [(xs, ys), (xs_dot, ys_dot)] = data_pass(8377) #iloveyou
    plot(xs, ys, xs_dot, ys_dot, 'pink')

    [(xs, ys), (xs_dot, ys_dot)] = data_pass(48) #iwillsurvive
    plot(xs, ys, xs_dot, ys_dot, 'yellow')

    [(xs, ys), (xs_dot, ys_dot)] = data_pass(2)
    plot(xs, ys, xs_dot, ys_dot, 'green')

    [(xs, ys), (xs_dot, ys_dot)] = data_pass(1) #min pass
    plot(xs, ys, xs_dot, ys_dot, 'blue')

    plt.legend(['"123456" #1', '"iloveyou" #22', '"iwillsurvive" #50757', 'Mean', 'The last'])
    plt.savefig(get_path(path))


num_pass = 57950593
num_unique = 36004082
plot_file = './analysis/certainty.png'

run(num_pass, num_unique, plot_file)
