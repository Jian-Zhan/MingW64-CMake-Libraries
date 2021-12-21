#include <iostream>

#include "hdf5.h"

#include <opencv2/opencv.hpp>

#include "QSICameraCLib.h"
#include "QSIError.h"

#include "mainwindow.h"
#include <QApplication>

int main(int argc, char** argv) {
    std::cout << "Hello, world ...";
    
    H5open();
    H5close();

    cv::Mat image(7, 7, CV_32FC2, cv::Scalar(1,3));
    image.create(100, 60, CV_8UC(15));

    qsi_handle handle;
	qsi_status status;
    status = QSI_CreateHandle(&handle);
    QSI_ReleaseHandle(handle);
    
    std::cout << " done." << std::endl;

    QApplication a(argc, argv);
    MainWindow w;
    w.show();
    return a.exec();
}