//
//  ContentView.swift
//  fxUI
//
//  Created by Roman Zheglov on 19.09.2021.
//

import SwiftUI

class Triangle
{
    var v1: Point
    var v2: Point
    var v3: Point
    
    init(v1: Point, v2: Point, v3: Point)
    {
        self.v1 = v1
        self.v2 = v2
        self.v3 = v3
    }
    
    func BuildTriangle() {}
}

struct ContentView: View {
    init()
    {
        
    }
    @State var lastPoint : Point = Point(0, 0)
    @State var scale = 20
    @State var pixelsPoint: [Point] = [
        
    ]
    var body: some View {
        GeometryReader
        {
            geometry in
            ZStack {
                Button(action: {
                    let currentHeight = Int(geometry.size.height) / scale - 1
                    let currentWidth = Int(geometry.size.width) / scale
                    
                    var triangle : Triangle = Triangle(
                        v1: Point(0, currentHeight),
                        v2: Point(currentWidth / 2,0),
                        v3: Point(currentWidth, currentHeight))
                    var points: [Point] = DrawLineBeresenham(triangle.v1, triangle.v2)
                    points.append(contentsOf: DrawLineBeresenham(triangle.v2, triangle.v3))
                    points.append(contentsOf: DrawLineACDA(triangle.v1, triangle.v3))
                    pixelsPoint.append(contentsOf: points)
                }, label: {
                    Text("\(geometry.size.width) x \(geometry.size.height)")
                }).position(x: 90, y: 15)
                Spacer()
                
                ZStack(alignment: .topLeading)
                {
                    ForEach(pixelsPoint, id: \.self)
                    {
                        item in
                        Rectangle()
                            .fill(!item.isGreen ? Color.green : Color.red)
                            .offset(x: CGFloat(item.x * scale), y: CGFloat(item.y * scale))
                            .frame(width: CGFloat(scale), height: CGFloat(scale), alignment: .leading)
                    }
                }.position(x: CGFloat(scale/2), y: CGFloat(scale/2))
            }
        }
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
