# import required modules
#from signal import SIGABRT
#from typing_extensions import Self
#from zlib import Z_RLE
#import matplotlib.pyplot as plt
import numpy as np
from scipy import signal
import csv

#用csv獨進來
a = [] #所以你寫得時候這邊就是input的紅光參數
b = [] #input的紅外光參數
c = [] #input的x綠光參數
with open('Data/WriteCsv.csv', newline='') as csvfile:
    rows = csv.DictReader(csvfile)
    for row in rows:
        a.append(row['channel3']) #Red
        b.append(row['channel2']) #IR
        c.append(row['channel1']) #Green
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
wp = fp/(f_sample/2)
ws = fs/(f_sample/2)

f_cut = 4
f_cuts = 7
wp_c = f_cut/(f_sample/2)
wp_s = f_cuts/(f_sample/2)

N, Wn = signal.buttord(wp, ws, 0.01, 20)
N1, Wn1 = signal.buttord(wp_c, wp_s, 0.01, 20)
#N1, Wn1 = signal.buttord()
#print('Order of the filter=', N)
#print('Cut-off frequency=', Wn)

b, a = signal.butter(N, Wn, 'bandpass')
#print(b)
#print(a)
sos = signal.butter(N, Wn, 'bandpass', output='sos')

def moving_average(a, n) :
    ret = np.cumsum(a, dtype=float)
    ret[n:] = ret[n:] - ret[:-n]
    return ret[n - 1:] / n
filtered_R = signal.sosfiltfilt(sos, x)
x_new = moving_average(filtered_R, 20)
filtered_IR = signal.sosfiltfilt(sos, y)
y_new = moving_average(filtered_IR, 20)
filtered_G = signal.sosfiltfilt(sos, z)
z_new = moving_average(filtered_G, 5)

ans = []
ans_new = []
# print(len(z), len(z_new))
for i in range(0, len(x) - 149):
    R_AC = np.sqrt(np.mean(x_new[i:i+149]**2))
    R_DC = np.mean(x[i+19:i+168])    
    R1 = R_AC / R_DC

    IR_AC = np.sqrt(np.mean(y_new[i:i+149]**2))
    IR_DC = np.mean(y[i+19:i+168])
    R2 = IR_AC / IR_DC
    R = R1 / R2
    ans.append(110 - 25 * R)
    ans_new.append(-16.666666*(R**2) + 8.333333*R + 100)



ans_new = moving_average(ans_new, 400)
# ans = moving_average(ans, 400)
# working_data, measures = hp.process(z_new[0:500], 50, report_time=True)

# fig = plt.figure(figsize=(10, 6))
# sub = plt.subplot(4,2,1)
# sub.plot(x[0:700])
# sub.set_title('Original IR', fontsize=12)
# sub.grid()
# sub1 = plt.subplot(4,2,2)
# sub1.plot(y[0:700])
# sub1.set_title('Original Red', fontsize=12)
# sub1.grid()
# sub2 = plt.subplot(4,2,3)
# sub2.plot(filtered_R[0:700])
# sub2.set_title('IR fitered with butter', fontsize=12)
# sub2.grid()
# sub3 = plt.subplot(4,2,4)
# sub3.plot(filtered_IR[0:700])
# sub3.set_title('Red filtered with butter', fontsize=12)
# sub3.grid()
# sub4 = plt.subplot(4,2,5)
# sub4.plot(x_new[0:700])
# sub4.set_title('After MAF (IR)', fontsize=12)
# sub4.grid()
# sub5 = plt.subplot(4,2,6)
# sub5.plot(y_new[0:700])
# sub5.set_title('After MAF (Red)', fontsize=12)
# sub5.grid()
# sub6 = plt.subplot(4,2,7)
# sub6.plot(ans)
# sub6.set_title('110 - 25*R', fontsize=12)
# sub6.grid()
# sub7 = plt.subplot(4,2,8)
# sub7.plot(ans_new)
# sub7.set_title('a * R^2 + b * R + c', fontsize=12)
# sub7.grid()
# plt.subplots_adjust(hspace=0.5)
# fig.tight_layout()
# plt.show()

test = [1,2,3,4,5,6,7,8,9,10]
#print(z_new)
def first_derivatvie(a):
    c = np.diff(a)
    return c
c = first_derivatvie(z_new)
#print(c)
d = (c - c.mean())/(c.std())
# = d[0:500]
# print(X)
# def standardize(c):
#     return (c[:] - np.mean(c))/(np.std(c))
# d = standardize(c)
def hl_envelopes_idx(s, dmin=1, dmax=1, split=False):
    """
    Input :
    s: 1d-array, data signal from which to extract high and low envelopes
    dmin, dmax: int, optional, size of chunks, use this if the size of the input signal is too big
    split: bool, optional, if True, split the signal in half along its mean, might help to generate the envelope in some cases
    Output :
    lmin,lmax : high/low envelope idx of input signal s
    """

    # locals min      
    lmin = (np.diff(np.sign(np.diff(s))) > 0).nonzero()[0] + 1 
    # locals max
    lmax = (np.diff(np.sign(np.diff(s))) < 0).nonzero()[0] + 1 
    

    if split:
        # s_mid is zero if s centered around x-axis or more generally mean of signal
        s_mid = np.mean(s) 
        # pre-sorting of locals min based on relative position with respect to s_mid 
        lmin = lmin[s[lmin]<s_mid]
        # pre-sorting of local max based on relative position with respect to s_mid 
        lmax = lmax[s[lmax]>s_mid]


    # global max of dmax-chunks of locals max 
    lmin = lmin[[i+np.argmin(s[lmin[i:i+dmin]]) for i in range(0,len(lmin),dmin)]]
    # global min of dmin-chunks of locals min 
    lmax = lmax[[i+np.argmax(s[lmax[i:i+dmax]]) for i in range(0,len(lmax),dmax)]]
    
    return lmin,lmax


t = np.array([int(n) for n in range(0,len(d))])
#print(t)
high_idx, low_idx = hl_envelopes_idx(d)
#print(low_idx)
# plt.plot(t,d,label='signal')
# plt.plot(t[high_idx], d[high_idx], 'r', label='low')
# plt.plot(t[low_idx], d[low_idx], 'g', label='high')
# plt.show()
hr_array = signal.argrelextrema(d[low_idx], np.less)
hr_array1 = signal.argrelextrema(d[high_idx], np.greater)
hr_array = hr_array[0]
hr_array1 = hr_array1[0]
# print(len(hr_array1))

hr = []
hr1 = []
for i in range(1,len(hr_array) - 1):
    a = (low_idx[hr_array[i]] - low_idx[hr_array[i - 1]]) / 50
    temp = (low_idx[hr_array[i]] - low_idx[hr_array[i - 1]])/50
    ans = (1 / temp) * 60
    hr.append(ans)
for i in range(1,len(hr_array1) - 1):
    a = (high_idx[hr_array1[i]] - high_idx[hr_array1[i - 1]]) / 50
    temp = (high_idx[hr_array1[i]] - high_idx[hr_array1[i - 1]])/50
    ans = (1 / temp) * 60
    hr1.append(ans)
# print(hr)
# print(hr1)
hr = moving_average(hr, 5)
hr1 = moving_average(hr1, 5)

print(';', ans_new[-1].round(2),';',hr1[-1].round(0),end="")
# fig = plt.figure(figsize=(10, 6))
# sub = plt.subplot(2,1,1)
# sub.plot(z)
# sub.set_title('Original Green', fontsize=12)
# sub.grid()
# sub1 = plt.subplot(2,1,2)
# sub1.plot(hr1,'g')
# sub1.set_title('Heart rate', fontsize=12)
# sub1.grid()
# plt.subplots_adjust(hspace=0.5)
# fig.tight_layout()
# plt.show()




    

# fig = plt.figure(figsize=(10, 6))
# sub = plt.subplot(4,2,1)
# sub.plot(x[0:700])
# sub.set_title('Original IR', fontsize=12)
# sub.grid()
# sub1 = plt.subplot(4,2,2)
# sub1.plot(y[0:700])
# sub1.set_title('Original Red', fontsize=12)
# sub1.grid()
# sub2 = plt.subplot(4,2,3)
# sub2.plot(filtered_R[0:700])
# sub2.set_title('IR fitered with butter', fontsize=12)
# sub2.grid()
# sub3 = plt.subplot(4,2,4)
# sub3.plot(filtered_IR[0:700])
# sub3.set_title('Red filtered with butter', fontsize=12)
# sub3.grid()
# sub4 = plt.subplot(4,2,5)
# sub4.plot(x_new[0:700])
# sub4.set_title('After MAF (IR)', fontsize=12)
# sub4.grid()
# sub5 = plt.subplot(4,2,6)
# sub5.plot(y_new[0:700])
# sub5.set_title('After MAF (Red)', fontsize=12)
# sub5.grid()
# sub6 = plt.subplot(4,2,7)
# sub6.plot(ans)
# sub6.set_title('110 - 25*R', fontsize=12)
# sub6.grid()
# sub7 = plt.subplot(4,2,8)
# sub7.plot(ans_new)
# sub7.set_title('a * R^2 + b * R + c', fontsize=12)
# sub7.grid()
# plt.subplots_adjust(hspace=0.5)
# fig.tight_layout()
# plt.show()


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

'''
def mfreqz(b, a, Fs):
   
    # Compute frequency response of the filter
    # using signal.freqz function
    wz, hz = signal.freqz(b, a)
 
    # Calculate Magnitude from hz in dB
    Mag = 20*np.log10(abs(hz))
 
    # Calculate phase angle in degree from hz
    Phase = np.unwrap(np.arctan2(np.imag(hz), np.real(hz)))*(180/np.pi)
     
    # Calculate frequency in Hz from wz
    Freq = wz*Fs/(2*np.pi)
     
    # Plot filter magnitude and phase responses using subplot.
    fig = plt.figure(figsize=(10, 6))
 
    # Plot Magnitude response
    sub1 = plt.subplot(2, 1, 1)
    sub1.plot(Freq, Mag, 'r', linewidth=2)
    sub1.axis([0, Fs/2, -100, 5])
    sub1.set_title('Magnitude Response', fontsize=20)
    sub1.set_xlabel('Frequency [Hz]', fontsize=20)
    sub1.set_ylabel('Magnitude [dB]', fontsize=20)
    sub1.grid()
 
    # Plot phase angle
    sub2 = plt.subplot(2, 1, 2)
    sub2.plot(Freq, Phase, 'g', linewidth=2)
    sub2.set_ylabel('Phase (degree)', fontsize=20)
    sub2.set_xlabel(r'Frequency (Hz)', fontsize=20)
    sub2.set_title(r'Phase response', fontsize=20)
    sub2.grid()
'''


