//
//  ContentView.swift
//  fxUI2
//
//  Created by Roman Zheglov on 20.10.2021.
//

import SwiftUI

func buildCanvas(startPoint: CGPoint) -> Canvas
{
    var result = Canvas()
    let hour = DynamicFigure()
    hour.add(point: CGPoint(x: 0, y: 0))
    hour.add(point: CGPoint(x: 0,y: 50))
    let second = DynamicFigure()
    second.add(point: CGPoint(x: 0, y: 0))
    second.add(point: CGPoint(x: 0,y: 150))
    let minute = DynamicFigure()
    minute.add(point: CGPoint(x: 0, y: 0))
    minute.add(point: CGPoint(x: 0,y: 100))
    let alarm = DynamicFigure()
    alarm.add(point: CGPoint(x: 0, y: 0))
    alarm.add(point: CGPoint(x: 0,y: 30))
    result.add(figure: hour)
    result.add(figure: minute)
    result.add(figure: second)
    result.add(figure: alarm)
    return result
}

struct ContentView: View {
    
    @ObservedObject var canvas: Canvas = Canvas()
    
    @State private var time: String = "EEEE"
    
    @State var scale:Int = 1
    @State var centerPoint = CGPoint(x: 0,y: 0)
    @State var frameNumber: Int = 0
    
    @State var show:Bool = false
    
    @State var haltText: String = "!"
    
    
    var body: some View {
        Path { path in
            path.move(to: CGPoint(x: 0, y: 0))
            for item in canvas.elements {
                path.addLines(item.composition)
                path.closeSubpath()
            }
        }
        .stroke(Color.blue, lineWidth: 10)
        Button("Press", action:
                {
            canvas.joinCanvas(canvas: buildCanvas(startPoint: CGPoint(x: 0,y: 0)))
            canvas[0].move(by: CGPoint(x: 100,y: 100))
            canvas[1].move(by: CGPoint(x: 100,y: 100))
            canvas[2].move(by: CGPoint(x: 100,y: 100))
            canvas[3].move(by: CGPoint(x: 100,y: 100))
            canvas[3].rotate(angle: Double(180), from: CGPoint(x: 100, y: 100))
            let cal = Calendar.current
            let sec: Int = cal.component(.second, from: Date())
            let min: Int = cal.component(.minute, from: Date())
            let hou: Int = cal.component(.hour, from: Date())
            canvas[2].rotate(angle: Double(6*sec+180), from: CGPoint(x: 100, y: 100))
            canvas[1].rotate(angle: Double(6*min+180), from: CGPoint(x: 100, y: 100))
            canvas[0].rotate(angle: Double(30*hou+180), from: CGPoint(x: 100, y: 100))
            let _ = Timer.scheduledTimer(withTimeInterval: 1, repeats: true) { timer in
                canvas[2].rotate(angle: 6, from: CGPoint(x: 100, y: 100))
            }
            let _ = Timer.scheduledTimer(withTimeInterval: 60, repeats: true) { timer in
                canvas[1].rotate(angle: 6, from: CGPoint(x: 100, y: 100))
            }
            let _ = Timer.scheduledTimer(withTimeInterval: 3600, repeats: true) { timer in
                canvas[0].rotate(angle: 6, from: CGPoint(x: 100, y: 100))
            }
            
        })
        Text(haltText)
        TextField("Time", text: $time)
        Button("Press", action:
        {
            let cal = Calendar.current
            let sec: Int = cal.component(.second, from: Date())
            canvas[3].rotate(angle: Double(30*sec+180)+Double(time)!, from: CGPoint(x: 100, y: 100))
            DispatchQueue.main.asyncAfter(deadline: .now() + Double(time)!) {
                haltText = "HALT"
            }

        })
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
