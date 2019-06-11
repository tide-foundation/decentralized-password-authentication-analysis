import datetime
import pandas as pd
import numpy as np
import gc
import os

def get_path(file):
    work_folder = os.path.abspath('../data/split')
    return os.path.abspath(work_folder + '/' + file)

num_files = 4
account_file = './accounts-{0}.txt'
password_file = './passwords-{0}.txt'
output_file = './joined-{0}.txt'

hash_columns = ['hash', 'mail']
pass_columns = ['hash', 'pass']

for i in range(num_files):
    df_hash = pd.read_csv(get_path(account_file).format(i), header = None, names = hash_columns, index_col = 0)
    df_pass = pd.read_csv(get_path(password_file).format(i), header=None, names=pass_columns, index_col=0)

    df = df_hash.join(df_pass)
    df = df[~df.index.isnull()].dropna(axis=0)
    df.sort_values(by=['pass', 'mail']).to_csv(get_path(output_file).format(i), index=False, header=False)

    del df_hash
    del df_pass
    del df
    gc.collect()

