# import required modules
from signal import SIGABRT
import numpy as np
import matplotlib.pyplot as plt
from scipy import signal
import csv
import math
import heartpy as hp
import sys

# 用csv獨進來
a = []  # 所以你寫得時候這邊就是input的紅光參數
b = []  # input的紅外光參數
c = []  # input的x綠光參數
with open('WriteCsv.csv', newline='') as csvfile:
    rows = csv.DictReader(csvfile)
    for row in rows:
        a.append(row['channel3'])  # Red
        b.append(row['channel2'])  # IR
        c.append(row['channel1'])  # Green
# print(type(a))
f_sample = 50

x = [int(x) for x in a]
y = [int(y) for y in b]
z = [int(z) for z in c]

x = np.array(a)
y = np.array(b)
z = np.array(c)
x = x.astype(np.float64)
y = y.astype(np.float64)
z = z.astype(np.float64)

fp = np.array([0.6, 4])
fs = np.array([0.35, 7])
wp = fp / (f_sample / 2)
ws = fs / (f_sample / 2)

f_cut = 0.6
f_cuts = 0.28
wp_c = f_cut / (f_sample / 2)
wp_s = f_cuts / (f_sample / 2)

N, Wn = signal.buttord(wp, ws, 0.01, 20)
N1, Wn1 = signal.buttord(wp_c, wp_s, 0.01, 20)
# N1, Wn1 = signal.buttord()
# print('Order of the filter=', N)
# print('Cut-off frequency=', Wn)

b, a = signal.butter(N, Wn, 'bandpass')
# print(b)
# print(a)
sos = signal.butter(N, Wn, 'bandpass', output='sos')
# sos1 = signal.butter(N1, Wn1, 'highpass', output='sos')


def moving_average(a, n):
    ret = np.cumsum(a, dtype=float)
    ret[n:] = ret[n:] - ret[:-n]
    return ret[n - 1:] / n

filtered_R = signal.sosfiltfilt(sos, x)
x_new = moving_average(filtered_R, 20)
filtered_IR = signal.sosfiltfilt(sos, y)
y_new = moving_average(filtered_IR, 20)
filtered_G = signal.sosfiltfilt(sos, z)
z_new = moving_average(filtered_G, 10)


ans = []
ans_new = []
# print(len(z), len(z_new))
for i in range(0, len(x) - 149):
    R_AC = np.sqrt(np.mean(x_new[i:i + 149] ** 2))
    R_DC = np.mean(x[i + 19:i + 168])
    R1 = R_AC / R_DC

    IR_AC = np.sqrt(np.mean(y_new[i:i + 149] ** 2))
    IR_DC = np.mean(y[i + 19:i + 168])
    R2 = IR_AC / IR_DC
    R = R1 / R2
    ans.append(110 - 25 * R)
    ans_new.append(-16.666666 * (R ** 2) + 8.333333 * R + 100)

ans_new = moving_average(ans_new[0:500], 500)
ans = moving_average(ans[0:500], 500)
working_data, measures = hp.process(z_new[0:500], 50, report_time=True)
print(';', measures['bpm'].round(0),';', ans_new[0].round(2),end="")  # measures['bpm']就是HR
#print(';', ans_new[0].round(2))  # ans_new 就是SpO2

'''
fig = plt.figure(figsize=(10, 6))
sub = plt.subplot(4,2,1)
sub.plot(x[0:700])
sub.set_title('Original IR', fontsize=12)
sub.grid()
sub1 = plt.subplot(4,2,2)
sub1.plot(y[0:700])
sub1.set_title('Original Red', fontsize=12)
sub1.grid()
sub2 = plt.subplot(4,2,3)
sub2.plot(filtered_R[0:700])
sub2.set_title('IR fitered with butter', fontsize=12)
sub2.grid()
sub3 = plt.subplot(4,2,4)
sub3.plot(filtered_IR[0:700])
sub3.set_title('Red filtered with butter', fontsize=12)
sub3.grid()
sub4 = plt.subplot(4,2,5)
sub4.plot(x_new[0:700])
sub4.set_title('After MAF (IR)', fontsize=12)
sub4.grid()
sub5 = plt.subplot(4,2,6)
sub5.plot(y_new[0:700])
sub5.set_title('After MAF (Red)', fontsize=12)
sub5.grid()
sub6 = plt.subplot(4,2,7)
sub6.plot(ans)
sub6.set_title('110 - 25*R', fontsize=12)
sub6.grid()
sub7 = plt.subplot(4,2,8)
sub7.plot(ans_new)
sub7.set_title('a * R^2 + b * R + c', fontsize=12)
sub7.grid()
plt.subplots_adjust(hspace=0.5)
fig.tight_layout()
plt.show()
'''

'''
sub1 = plt.subplot(2, 1, 1)
sub1.plot(x)
sub1.grid()
sub2 = plt.subplot(2, 1, 2)
sub2.plot(filtered)
sub2.grid()
plt.show()

sub = plt.subplot(3, 1, 1)
sub.plot(x[0:750])
sub.set_title('original signal', fontsize=20)
sub.grid()
sub1 = plt.subplot(3, 1, 2)
sub1.plot(filtered[0:750])
sub1.set_title('filtered signal with Butter', fontsize=20)
sub1.grid()
sub2 = plt.subplot(3, 1, 3)
sub2.plot(x_new[0:750])
sub2.set_title('After moving average', fontsize=20)
sub2.grid()

plt.subplots_adjust(hspace=0.5)
plt.show()
'''


# def mfreqz(b, a, Fs):
#
#     # Compute frequency response of the filter
#     # using signal.freqz function
#     wz, hz = signal.freqz(b, a)
#
#     # Calculate Magnitude from hz in dB
#     Mag = 20*np.log10(abs(hz))
#
#     # Calculate phase angle in degree from hz
#     Phase = np.unwrap(np.arctan2(np.imag(hz), np.real(hz)))*(180/np.pi)
#
#     # Calculate frequency in Hz from wz
#     Freq = wz*Fs/(2*np.pi)
#
#     # Plot filter magnitude and phase responses using subplot.
#     fig = plt.figure(figsize=(10, 6))
#
#     # Plot Magnitude response
#     sub1 = plt.subplot(2, 1, 1)
#     sub1.plot(Freq, Mag, 'r', linewidth=2)
#     sub1.axis([0, Fs/2, -100, 5])
#     sub1.set_title('Magnitude Response', fontsize=20)
#     sub1.set_xlabel('Frequency [Hz]', fontsize=20)
#     sub1.set_ylabel('Magnitude [dB]', fontsize=20)
#     sub1.grid()
#
#     # Plot phase angle
#     sub2 = plt.subplot(2, 1, 2)
#     sub2.plot(Freq, Phase, 'g', linewidth=2)
#     sub2.set_ylabel('Phase (degree)', fontsize=20)
#     sub2.set_xlabel(r'Frequency (Hz)', fontsize=20)
#     sub2.set_title(r'Phase response', fontsize=20)
#     sub2.grid()
#     plt.show()
#
#
# mfreqz(c, d, f_sample)

