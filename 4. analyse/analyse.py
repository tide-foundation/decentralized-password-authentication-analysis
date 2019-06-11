import pandas as pd
import numpy as np
import os

def get_path(file):
    work_folder = os.path.abspath('../data')
    return os.path.abspath(work_folder + '/' + file)

pass_file = './processedPasswords.txt'
output_name = './analysis/analysis-{0}.txt'

cols_to_use = [0, 1, 2]
column_names = ['user', 'pass', 'hash']
df = (pd.read_csv(get_path(pass_file), header=None, names=column_names, usecols=cols_to_use))
df = df.dropna(axis=0)

grp_pass = df[['user', 'pass']].groupby('pass').count()
grp_pass.columns = ['pass_count']
grp_pass['pass%'] = grp_pass['pass_count'] / len(df) * 100

grp_hash = df[['pass', 'hash']].groupby('hash').count()
grp_hash.columns = ['hash_count']
grp_hash['hash%'] = grp_hash['hash_count'] / len(df) * 100

df = df[~df.set_index('pass').index.duplicated(keep='first')]
df = df.join(grp_pass, on='pass')
df = df.join(grp_hash, on='hash')

df['certainty%'] = df['pass_count'] / df['hash_count'] * 100
df['increase%'] = (df['hash_count'] / df['pass_count'] * 100) - 100
with open(get_path(output_name).format('mean'), 'w') as outfile:
    df[['pass_count', 'pass%', 'hash_count', 'hash%',
        'certainty%', 'increase%']].mean().to_string(outfile)

df = df.sort_values(by=['pass_count', 'pass'], ascending=False)
df.to_csv(get_path(output_name).format('unique%'), index=False)

grp = df[['hash', 'pass_count']].groupby('hash')
tmp = grp.sum()
tmp.columns = ['#pass']
tmp['#unique'] = grp.count()['pass_count']
tmp['max'] = grp.max()['pass_count']
tmp['min'] = grp.min()['pass_count']
tmp['mean'] = grp.mean()['pass_count']
tmp.to_csv(get_path(output_name).format('group'))
