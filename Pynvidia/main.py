from pynvml import *


class GPUInfo:
    def __init__(self):
        try:
            nvmlInit()
        except NVMLError as Err:
            print(f"NVML 초기화 오류: {Err}")

    def shutdown(self):
        try:
            nvmlShutdown()
        except NVMLError as Err:
            print(f"NVML 종료 오류: {Err}")

    def getGpuTemperatures(self):
        info = []
        try:
            gpu_count = nvmlDeviceGetCount()
            for i in range(gpu_count):
                handle = nvmlDeviceGetHandleByIndex(i)
                temp = nvmlDeviceGetTemperature(handle, NVML_TEMPERATURE_GPU)
                info.append((i, temp))
        except NVMLError as Err:
            print(f"GPU 온도 가져오기 오류: {Err}")
        return info


if __name__ == '__main__':
    gpuinfo = GPUInfo()
    temperatures = gpuinfo.getGpuTemperatures()
    for gpu_id, temp in temperatures:
        print(f"GPU {gpu_id}: 온도는 {temp}°C")
    gpuinfo.shutdown()
