//
//  FX.swift
//  fxUI2
//
//  Created by Roman Zheglov on 20.10.2021.
//

import Foundation
import SwiftUI

protocol Figure
{
    var composition: [CGPoint] {get}
}

class DynamicFigure : Figure, ObservableObject
{
    @Published var composition: [CGPoint] = []
    
    var observerSubs: [() -> Void] = []
    
    func move(by: CGPoint)
    {
        for i in 0..<composition.count {
            composition[i].x = composition[i].x + by.x
            composition[i].y = composition[i].y + by.y
        }
        onChange()
    }
    func scale(from: CGPoint, xScale: Double, yScale: Double)
    {
        for i in 0..<composition.count {
            composition[i] = scaleFromPoint(from: from, point: composition[i], xScale: xScale, yScale: yScale)
        }
        onChange()
    }
    func rotate(angle: Double, from: CGPoint)
    {
        for i in 0..<composition.count {
            composition[i] = rotateFromPoint(from: from, point: composition[i], angle: angle)
        }
        onChange()
        
    }
    func add(point: CGPoint)
    {
        composition.append(point)
        onChange()
    }
    func add(_ x: Int, _ y: Int)
    {
        add(point: CGPoint(x: x, y: y))
    }
    internal func onChange()
    {
        for item in observerSubs
        {
            item()
        }
    }
}

class Canvas : ObservableObject
{
    var elements: [DynamicFigure] = []
    
    var count: Int {
        get
        {
            return elements.count
        }
    }
    
    func add(figure: DynamicFigure) {
        figure.observerSubs.append(
            {
                self.objectWillChange.send()
            })
        elements.append(figure)
        self.objectWillChange.send()
    }
    
    func joinCanvas(canvas: Canvas) {
        for i in 0..<canvas.count {
            add(figure: canvas[i])
        }
        self.objectWillChange.send()
    }
    
    subscript(index: Int) -> DynamicFigure
    {
        get
        {
            return elements[index]
        }
    }
    
}
func rotateFromPoint(from:CGPoint, point:CGPoint, angle: Double) -> CGPoint
{
    let angleCos: Double = cos(Double(angle * Double.pi / 180))
    let angleSin: Double = sin(Double(angle * Double.pi / 180))
    let x: Double = Double(from.x) + Double(point.x - from.x) * angleCos - Double(point.y - from.y) * angleSin
    let y: Double = Double(from.y) + Double(point.x - from.x) * angleSin + Double(point.y - from.y) * angleCos
    return CGPoint(x: Int(x.rounded()), y: Int(y.rounded()))
}

func scaleFromPoint(from: CGPoint, point: CGPoint, xScale: Double, yScale: Double) -> CGPoint
{
    let x = from.x + xScale * (point.x - from.x)
    let y = from.y + yScale * (point.y - from.y)
    return CGPoint(x: x, y: y)
}

struct FrameInfo
{
    var anchor: CGPoint = CGPoint(x: 0, y: 0)
    var move: CGPoint = CGPoint(x: 0, y: 0)
    var scale: Double = 0
    var rotate: Double = 0
}

struct AnimationInfo
{
    var moveFrom: CGPoint = CGPoint(x: 0, y: 0)
    var moveTo: CGPoint = CGPoint(x: 0, y: 0)
    var scaleFrom: Double = 0
    var scaleTo: Double = 0
    var rotateFrom: Double = 0
    var rotateTo: Double = 0
    var duration: Int = 0
}

//class AnimateCanvas
//{
//    var canvas: Canvas
//    
//    var frameNumber: Int64 = 0
//    
//    var queue: [AnimationInfo] = []
//    
//    init (canvas: Canvas)
//    {
//        self.canvas = canvas
//    }
//    
//    func start()
//    {
//        DispatchQueue.main.asyncAfter(deadline: .now()) {
//            for item in self.queue {
//                let move = CGPoint(
//                    x: item.moveFrom.x - item.moveTo.x,
//                    y: item.moveFrom.y - item.moveTo.y)
//                let scale = item.scaleFrom - item.scaleTo
//                let rotate = item.rotateFrom - item.rotateTo
//                
//                let MXPS = item.duration / move.x
//                let MYPS = item.duration / move.y
//                let SPS = item.duration / scale
//                let RPS = item.duration / rotate
//                
//                for i in items {
//                    <#code#>
//                }
//            }
//        }
//    }
//    
//    func moveCanvas(aInfo: FrameInfo)
//    {
//        for item in canvas.elements {
//            item.move(by: aInfo.move)
//            item.scale(from: aInfo.anchor, xScale: aInfo.scale, yScale: aInfo.scale)
//            item.rotate(angle: aInfo.rotate, from: aInfo.anchor)
//        }
//    }
//}
