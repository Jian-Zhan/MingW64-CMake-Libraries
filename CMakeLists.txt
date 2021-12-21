cmake_minimum_required(VERSION 3.0.0)
project(helloworld VERSION 0.1.0)

set(CMAKE_VERBOSE_MAKEFILE ON)

set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)
set(CMAKE_AUTOUIC ON)

# Make this a GUI application on Windows
if(WIN32)
  set(CMAKE_WIN32_EXECUTABLE ON)
endif()

#include(CTest)
#enable_testing()

find_package(HDF5 REQUIRED)
find_package(OpenCV REQUIRED)
find_package(Qt5 COMPONENTS Widgets REQUIRED)

include_directories("${HDF5_INCLUDE_DIR}")
include_directories( ${OpenCV_INCLUDE_DIRS} )
include_directories("third_party/QSICameraCLib/include")

add_executable(helloworld
    src/main.cpp
    src/mainwindow.cpp
    src/mainwindow.ui
    src/resources.qrc
    )

target_link_libraries(helloworld "${HDF5_LIBRARIES}")
target_link_libraries(helloworld "${OpenCV_LIBS}")
target_link_libraries(helloworld Qt5::Widgets)
target_link_libraries(helloworld "${CMAKE_SOURCE_DIR}/third_party/QSICameraCLib/lib/x64/QSICameraCLib.dll")

add_custom_command(
        TARGET helloworld POST_BUILD
        COMMAND ${CMAKE_COMMAND} -E copy
                ${CMAKE_SOURCE_DIR}/third_party/QSICameraCLib/lib/x64/QSICameraCLib.dll
                ${CMAKE_CURRENT_BINARY_DIR}/QSICameraCLib.dll)

add_custom_command(
        TARGET helloworld POST_BUILD
        COMMAND ${CMAKE_COMMAND} -E copy
                ${CMAKE_SOURCE_DIR}/third_party/FTDIDriver/amd64/ftd2xx.dll
                ${CMAKE_CURRENT_BINARY_DIR}/ftd2xx.dll)

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)
