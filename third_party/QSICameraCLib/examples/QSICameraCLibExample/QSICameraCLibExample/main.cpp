#include <memory>
#include <cstdio>

#include <QSICameraCLib.h>
#include <QSIError.h>

int main(int argc, char** argv) {
	qsi_handle handle;
	qsi_status status;
	printf("Create Handle\n");
	status = QSI_CreateHandle(&handle);

	if (status != QSI_OK) {
		printf("Error Creating Handle\n");
		fgetc(stdin);

		return -1;
	}

	printf("Connecting\n");
	status = QSI_Connect(handle);

	if (status != QSI_OK) {
		printf("Error Connecting to camera. Code: %d\n", status);
		fgetc(stdin);

		return -1;
	}

	int x, y;
	QSI_GetImageNumX(handle, &x);
	QSI_GetImageNumY(handle, &y);
	int length = x * y;
	unsigned short* buffer = (unsigned short*)calloc(length, sizeof(unsigned short));

	printf("Starting Exposure\n");
	status = QSI_StartExposure(handle, 5, true);

	if (status != QSI_OK) {
		printf("Error starting exposure. Code: %d\n", status);
		fgetc(stdin);

		return -1;
	}

	printf("Waiting for image\n");
	qsi_bool ready = false;
	while (!ready) {
		status = QSI_GetImageReady(handle, &ready);
		if (status != QSI_OK) {
			printf("Error Waiting For Image. Code: %d\n", status);
			fgetc(stdin);

			return -1;
		}
	}

	printf("Downloading Image\n");
	status = QSI_ReadImage(handle, buffer, length);
	if (status != QSI_OK) {
		printf("Error Reading Image. Code: %d\n", status);
		fgetc(stdin);

		return -1;
	}

	printf("Image Downloaded\n");

	long val = 0;
	for (auto i = 0; i < length - 1; ++i) {
		val += (long)buffer[i];
	}
	val = val / length;
	printf("Average ADU is %d\n", val);
	QSI_ReleaseHandle(handle);

	fgetc(stdin);
	return 0;
}
