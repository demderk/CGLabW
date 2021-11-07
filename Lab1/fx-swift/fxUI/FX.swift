//
//  FX.swift
//  fxUI
//
//  Created by Roman Zheglov on 29.09.2021.
//

import Foundation
import SwiftUI

struct Point : Hashable {
    let x: Int
    let y: Int
    var scale: Int = 10
    var isGreen: Bool = false
    init(_ point: PointF)
    {
        x = Int(point.x.rounded())
        y = Int(point.y.rounded())
    }
    init(_ x: Int, _ y: Int)
    {
        self.x = x
        self.y = y
    }
    init(_ x: Int,_ y: Int, _ color: Bool)
    {
        self.init(x, y)
        isGreen = color
    }
}

struct PointF : Hashable {
    let x: CGFloat
    let y: CGFloat
    init(_ point: Point)
    {
        x = CGFloat(point.x)
        y = CGFloat(point.y)
    }
    init(_ x: CGFloat, _ y: CGFloat)
    {
        self.x = x
        self.y = y
    }
}

func DrawLineACDA(_ from: Point, _ to: Point) -> [Point]
{
    var result = [Point]()
    var pointA = from
    var pointB = to
    var dx = pointB.x - pointA.x
    var dy = pointB.y - pointA.y
    if (dx < 0 || dy < 0)
    {
        let temp = pointA
        pointA = pointB
        pointB = temp
        dx = pointB.x - pointA.x
        dy = pointB.y - pointA.y
    }
    var d : CGFloat;
    result.append(pointA)
    var position: PointF = PointF(pointA)
    if dx == 0 {
        d = 0;
        while (Point(position) != pointB) {
            position = PointF(position.x + d, position.y + 1)
            result.append(Point(position))
        }
    }
    else if dy == 0
    {
        d = 0;
        while (Point(position) != pointB) {
            position = PointF(position.x + 1, position.y + d)
            result.append(Point(position))
        }
    }
    else if dx >= dy
    {
        d = CGFloat(dy) / CGFloat(dx)
        while (Point(position) != pointB) {
            position = PointF(position.x + 1, position.y + d)
            result.append(Point(position))
        }
    }
    else
    {
        d = CGFloat(dx) / CGFloat(dy)
        while (Point(position) != pointB) {
            position = PointF(position.x + d, position.y + 1)
            result.append(Point(position))
        }
    }
    return result
}
func DrawLineBeresenham(_ from: Point, _ to: Point) -> [Point]
{
    var result: [Point] = []
    var pointA: Point = from
    var pointB: Point = to
    var dx = pointB.x - pointA.x
    var dy = pointB.y - pointA.y
    if (pointA.y > pointB.y)
    {
        let temp = pointA
        pointA = pointB
        pointB = temp
        dx = pointB.x - pointA.x
        dy = pointB.y - pointA.y
    }
    var deltaX:Int = -1
    if (dx >= 0)
    {
        deltaX = 1
    }
    else
    {
        deltaX = -1
        dx = dx * -1
    }
    var d = 0
    var t = 0
    var delta = 0
    if (dy >= dx)
    {
        t = dx << 1;
        delta = dy << 1;
    }
    else
    {
        t = dy << 1;
        delta = dx << 1;
    }
    var position: Point = pointA
    result.append(Point(position.x, position.y, true))
    if (dy >= dx)
    {
        while (position != pointB)
        {
            d += t
            if d > dy
            {
                position = Point(position.x + deltaX, position.y + 1)
                d -= delta;
            }
            else
            {
                position = Point(position.x, position.y + 1)
            }
            result.append(Point(position.x, position.y, true))
        }
        return result
    }
    else
    {
        while (position != pointB)
        {
            d += t
            if d > dx
            {
                position = Point(position.x + 1, position.y + 1)
                d -= delta;
            }
            else
            {
                position = Point(position.x + 1, position.y)
            }
            result.append(position)
        }
        return result;
    }
}
